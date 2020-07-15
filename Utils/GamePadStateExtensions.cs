using System;
using System.Reflection;
using Microsoft.Xna.Framework.Input;

namespace TAS.Utils
{
    // This class is used for serializing/deserializing the internal gamepad state
    // TODO: Change all of this reflection into something that runs faster
    public static class GamePadStateExtensions
    {
        private static readonly ConstructorInfo Ctor = typeof(GamePadState)
            .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
        private static readonly Type ErrorCodeType = Ctor.GetParameters()[1].ParameterType;

        private static readonly FieldInfo StateField = typeof(GamePadState)
            .GetField("_state", BindingFlags.Instance | BindingFlags.NonPublic);
        private static readonly Type StateType = StateField.FieldType;
        private static readonly FieldInfo StatePadField = StateType.GetField("GamePad");
        private static readonly Type GamePadType = StatePadField.FieldType;
        private static readonly FieldInfo GamePadButtons = GamePadType.GetField("Buttons");
        private static readonly FieldInfo GamePadLeftTrigger = GamePadType.GetField("LeftTrigger");
        private static readonly FieldInfo GamePadRightTrigger = GamePadType.GetField("RightTrigger");
        private static readonly FieldInfo GamePadThumbLX = GamePadType.GetField("ThumbLX");
        private static readonly FieldInfo GamePadThumbLY = GamePadType.GetField("ThumbLY");
        private static readonly FieldInfo GamePadThumbRX = GamePadType.GetField("ThumbRX");
        private static readonly FieldInfo GamePadThumbRY = GamePadType.GetField("ThumbRY");

        /// <summary>
        /// Gets the internal state of the given <see cref="GamePadState"/>
        /// </summary>
        /// <param name="self">The <see cref="GamePadState"/> to get the internal state of</param>
        /// <returns>An analogous object to the inaccessible internal state</returns>
        public static XINPUT_GAMEPAD GetInternalState(this GamePadState self)
        {
            object state = StatePadField.GetValue(StateField.GetValue(self));

            XINPUT_GAMEPAD result = default;
            result.Connected = self.IsConnected;
            result.Buttons = (ushort)GamePadButtons.GetValue(state);
            result.LeftTrigger = (byte)GamePadLeftTrigger.GetValue(state);
            result.RightTrigger = (byte)GamePadRightTrigger.GetValue(state);
            result.ThumbLX = (short)GamePadThumbLX.GetValue(state);
            result.ThumbLY = (short)GamePadThumbLY.GetValue(state);
            result.ThumbRX = (short)GamePadThumbRX.GetValue(state);
            result.ThumbRY = (short)GamePadThumbRY.GetValue(state);

            return result;
        }

        /// <summary>
        /// Creates a new <see cref="GamePadState"/> from the given <see cref="XINPUT_GAMEPAD"/>
        /// </summary>
        /// <param name="orig">The desired internal state of the returned <see cref="GamePadState"/></param>
        /// <param name="deadZone">The <see cref="GamePadDeadZone"/> to be used for the returnd <see cref="GamePadState"/></param>
        /// <returns>A new <see cref="GamePadState"/> with the given internal state</returns>
        public static GamePadState FromInternalState(XINPUT_GAMEPAD orig, GamePadDeadZone deadZone)
        {
            GamePadState result = default;
            object state = StateField.GetValue(result);
            object pad = StatePadField.GetValue(state);

            GamePadButtons.SetValue(pad, Enum.ToObject(GamePadButtons.FieldType, orig.Buttons));
            GamePadLeftTrigger.SetValue(pad, orig.LeftTrigger);
            GamePadRightTrigger.SetValue(pad, orig.RightTrigger);
            GamePadThumbLX.SetValue(pad, orig.ThumbLX);
            GamePadThumbLY.SetValue(pad, orig.ThumbLY);
            GamePadThumbRX.SetValue(pad, orig.ThumbRX);
            GamePadThumbRY.SetValue(pad, orig.ThumbRY);

            StatePadField.SetValue(state, pad);
            object errorCode = Enum.ToObject(ErrorCodeType, orig.Connected ? 0u : 997u);

            return (GamePadState)Ctor.Invoke(new object[] { state, errorCode, deadZone });
        }
    }

    /// <summary>
    /// Represents the inaccessible internal state of a <see cref="GamePadState"/>
    /// </summary>
    public struct XINPUT_GAMEPAD
    {
        public bool Connected;

        public ushort Buttons;
        public byte LeftTrigger;
        public byte RightTrigger;
        public short ThumbLX;
        public short ThumbLY;
        public short ThumbRX;
        public short ThumbRY;

        /// <summary>
        /// Creates a new <see cref="GamePadState"/> from the data in the target <see cref="XINPUT_GAMEPAD"/>
        /// </summary>
        /// <param name="deadZone">The <see cref="GamePadDeadZone"/> to be used for the returnd <see cref="GamePadState"/></param>
        /// <returns>A new <see cref="GamePadState"/> with the target internal state</returns>
        public GamePadState ConvertToGamePadState(GamePadDeadZone deadZone)
            => GamePadStateExtensions.FromInternalState(this, deadZone);
    }
}
