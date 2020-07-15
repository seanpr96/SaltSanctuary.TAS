using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoMod.RuntimeDetour;
using TAS.Patches.ProjectTower;
using TAS.Utils;

namespace TAS
{
    /// <summary>
    /// Handles recording and replay of game input
    /// </summary>
    public static class InputManager
    {
        private static BinaryWriter _inputWriter;

        private static InputFrame[] _frames;

        private static KeyboardState _kb;
        private static MouseState _mouse;

        private static XINPUT_GAMEPAD[] _pads = new XINPUT_GAMEPAD[4];
        private static readonly Dictionary<PlayerIndex, Dictionary<GamePadDeadZone, GamePadState>> PadCache
            = new Dictionary<PlayerIndex, Dictionary<GamePadDeadZone, GamePadState>>();

        public static Func<KeyboardState> GetActualKeyboard { get; private set; }
        public static Func<MouseState> GetActualMouse { get; private set; }
        public static Func<PlayerIndex, GamePadDeadZone, GamePadState> GetActualGamePad { get; private set; }

        public static InputState State { get; set; } = Config.InitialState;
        public static int CurrentFrame { get; private set; }

        public static void Initialize()
        {
            GetActualKeyboard = new Hook
            (
                typeof(Keyboard).GetMethod(nameof(Keyboard.GetState), new Type[0]),
                typeof(InputManager).GetMethod(nameof(GetKeyboardState))
            ).GenerateTrampoline<Func<KeyboardState>>();

            GetActualMouse = new Hook
            (
                typeof(Mouse).GetMethod(nameof(Mouse.GetState)),
                typeof(InputManager).GetMethod(nameof(GetMouseState))
            ).GenerateTrampoline<Func<MouseState>>();

            GetActualGamePad = new Hook
            (
                typeof(GamePad).GetMethod(nameof(GamePad.GetState),
                    new[] { typeof(PlayerIndex), typeof(GamePadDeadZone) }),
                typeof(InputManager).GetMethod(nameof(GetGamePadState))
            ).GenerateTrampoline<Func<PlayerIndex, GamePadDeadZone, GamePadState>>();

            Game1.Instance.OnUpdate += OnUpdate;

            if (State == InputState.Recording)
            {
                _inputWriter = new BinaryWriter(new FileStream("input.tas", FileMode.Create));
            }
            else if (State == InputState.Playback)
            {
                DeserializeInput();
            }
        }

        public static KeyboardState GetKeyboardState()
        {
            return _kb;
        }

        public static MouseState GetMouseState()
        {
            return _mouse;
        }

        public static GamePadState GetGamePadState(PlayerIndex i, GamePadDeadZone deadZone)
        {
            if (!PadCache.TryGetValue(i, out Dictionary<GamePadDeadZone, GamePadState> stateDict))
            {
                stateDict = new Dictionary<GamePadDeadZone, GamePadState>();
                PadCache[i] = stateDict;
            }

            if (!stateDict.TryGetValue(deadZone, out GamePadState state))
            {
                state = _pads[(int)i].ConvertToGamePadState(deadZone);
                stateDict[deadZone] = state;
            }

            return state;
        }

        private static void OnUpdate(GameTime gameTime)
        {
            switch (State)
            {
                case InputState.Recording:
                    OnUpdateRecording();
                    break;
                case InputState.Playback:
                    OnUpdatePlayback();
                    break;
                default:
                    OnUpdateNormal();
                    break;
            }
        }

        private static void OnUpdateRecording()
        {
            try
            {
                UpdateButtons();
            }
            finally
            {
                CurrentFrame++;
                SerializeInput();
                _inputWriter.Flush();
            }
        }

        private static void OnUpdatePlayback()
        {
            _kb = _frames[CurrentFrame].keyboard;
            _mouse = _frames[CurrentFrame].mouse;
            _pads = _frames[CurrentFrame].pads;

            for (int i = 0; i < 4; i++)
            {
                PadCache[(PlayerIndex)i]?.Clear();
            }

            CurrentFrame++;
            if (CurrentFrame >= _frames.Length)
            {
                State = InputState.Normal;
            }
        }

        private static void OnUpdateNormal()
        {
            UpdateButtons();
        }

        private static void UpdateButtons()
        {
            _kb = GetActualKeyboard();
            _mouse = GetActualMouse();
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex p = (PlayerIndex)i;
                _pads[i] = GetActualGamePad(p, GamePadDeadZone.None).GetInternalState();
                PadCache[p]?.Clear();
            }
        }

        private static void SerializeInput()
        {
            // Keyboard
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            _inputWriter.Write((byte)keys.Length);
            for (int i = 0; i < keys.Length; i++)
            {
                _inputWriter.Write((byte)keys[i]);
            }

            // Mouse
            MouseState mouse = Mouse.GetState();
            _inputWriter.Write(mouse.X);
            _inputWriter.Write(mouse.Y);
            _inputWriter.Write(mouse.ScrollWheelValue);

            byte mouseButtons = (byte)mouse.LeftButton;
            mouseButtons |= (byte)((byte)mouse.MiddleButton << 1);
            mouseButtons |= (byte)((byte)mouse.RightButton << 2);
            mouseButtons |= (byte)((byte)mouse.XButton1 << 3);
            mouseButtons |= (byte)((byte)mouse.XButton2 << 4);
            _inputWriter.Write(mouseButtons);

            // GamePads
            for (int i = 0; i < 4; i++)
            {
                XINPUT_GAMEPAD pad = _pads[i];
                _inputWriter.Write(pad.Connected);
                _inputWriter.Write(pad.Buttons);
                _inputWriter.Write(pad.LeftTrigger);
                _inputWriter.Write(pad.RightTrigger);
                _inputWriter.Write(pad.ThumbLX);
                _inputWriter.Write(pad.ThumbLY);
                _inputWriter.Write(pad.ThumbRX);
                _inputWriter.Write(pad.ThumbRY);
            }
        }

        private static void DeserializeInput()
        {
            using BinaryReader reader = new BinaryReader(new FileStream("input.tas", FileMode.OpenOrCreate));
            List<InputFrame> frames = new List<InputFrame>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                frames.Add(DeserializeFrame(reader));
            }

            _frames = frames.ToArray();
            if (_frames.Length == 0)
            {
                State = InputState.Normal;
            }
        }

        private static InputFrame DeserializeFrame(BinaryReader reader)
        {
            // Keyboard
            byte keyLength = reader.ReadByte();
            Keys[] keys = new Keys[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                keys[i] = (Keys)reader.ReadByte();
            }

            KeyboardState keyboard = new KeyboardState(keys);

            // Mouse
            int mouseX = reader.ReadInt32();
            int mouseY = reader.ReadInt32();
            int mouseScroll = reader.ReadInt32();
            byte mouseButtons = reader.ReadByte();

            ButtonState leftButton = ((mouseButtons & 0b00001) == 0) ? ButtonState.Released : ButtonState.Pressed;
            ButtonState middleButton = ((mouseButtons & 0b00010) == 0) ? ButtonState.Released : ButtonState.Pressed;
            ButtonState rightButton = ((mouseButtons & 0b00100) == 0) ? ButtonState.Released : ButtonState.Pressed;
            ButtonState xButton1 = ((mouseButtons & 0b01000) == 0) ? ButtonState.Released : ButtonState.Pressed;
            ButtonState xButton2 = ((mouseButtons & 0b10000) == 0) ? ButtonState.Released : ButtonState.Pressed;

            MouseState mouse = new MouseState(mouseX, mouseY, mouseScroll, leftButton, middleButton, rightButton, xButton1, xButton2);

            // Gamepads
            XINPUT_GAMEPAD[] pads = new XINPUT_GAMEPAD[4];
            for (int i = 0; i < 4; i++)
            {
                pads[i].Connected = reader.ReadBoolean();
                pads[i].Buttons = reader.ReadUInt16();
                pads[i].LeftTrigger = reader.ReadByte();
                pads[i].RightTrigger = reader.ReadByte();
                pads[i].ThumbLX = reader.ReadInt16();
                pads[i].ThumbLY = reader.ReadInt16();
                pads[i].ThumbRX = reader.ReadInt16();
                pads[i].ThumbRY = reader.ReadInt16();
            }

            // Create frame
            InputFrame frame = default;
            frame.keyboard = keyboard;
            frame.mouse = mouse;
            frame.pads = pads;
            return frame;
        }

        private struct InputFrame
        {
            public KeyboardState keyboard;
            public MouseState mouse;
            public XINPUT_GAMEPAD[] pads;
        }
    }

    public enum InputState
    {
        Normal,
        Recording,
        Playback
    }
}
