using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

namespace InputFileEditor
{
    public partial class InputEditor : Form
    {
        private List<InputFrame> _frames;

        public InputEditor()
        {
            InitializeComponent();

            MenuItem save = new MenuItem("Save", SaveToFile, Shortcut.CtrlS);
            MenuItem load = new MenuItem("Load", LoadFromFile, Shortcut.CtrlO);

            MenuItem file = new MenuItem("File", new[] { save, load });

            Menu = new MainMenu(new[] { file });

            _frames = new List<InputFrame>();
            _frames.Add(new InputFrame(1));

            UpdateFrameBox();

            frameBox.SelectedIndex = 0;
        }

        private void UpdateFrameBox()
        {
            frameBox.Items.Clear();
            int frameCount = 0;
            foreach (InputFrame frame in _frames)
            {
                if (frame.Length == 1)
                {
                    frameBox.Items.Add($"{frameCount}");
                }
                else
                {
                    frameBox.Items.Add($"{frameCount} - {frameCount + frame.Length - 1}");
                }

                frameCount += frame.Length;
            }
        }

        private void SaveValues(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= _frames.Count)
            {
                return;
            }

            InputFrame frame = new InputFrame((int)frameLengthInput.Value);

            List<XnaKeys> keys = new List<XnaKeys>();
            foreach (string key in keysBox.Items)
            {
                keys.Add((XnaKeys)Enum.Parse(typeof(XnaKeys), key));
            }

            frame.Keys = keys.ToArray();
            frame.MouseX = (int)mouseXInput.Value;
            frame.MouseY = (int)mouseYInput.Value;
            frame.MouseScroll = (int)mouseScrollInput.Value;
            frame.MouseLeft = mouseLeftBox.Checked;
            frame.MouseMiddle = mouseMiddleBox.Checked;
            frame.MouseRight = mouseRightBox.Checked;
            frame.Mouse4 = mouse4Box.Checked;
            frame.Mouse5 = mouse5Box.Checked;

            int oldFrameCount = _frames[frameIndex].Length;
            _frames[frameIndex] = frame;

            if (oldFrameCount != frame.Length)
            {
                int oldSel = frameBox.SelectedIndex;
                UpdateFrameBox();
                frameBox.SelectedIndex = oldSel;
            }
        }

        private void SetValues(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= _frames.Count)
            {
                return;
            }

            InputFrame frame = _frames[frameIndex];

            frameLengthInput.Value = frame.Length;

            keysBox.Items.Clear();
            keysBox.Items.AddRange(frame.Keys.Select(key => key.ToString()).ToArray());

            mouseXInput.Value = frame.MouseX;
            mouseYInput.Value = frame.MouseY;
            mouseScrollInput.Value = frame.MouseScroll;
            mouseLeftBox.Checked = frame.MouseLeft;
            mouseMiddleBox.Checked = frame.MouseMiddle;
            mouseRightBox.Checked = frame.MouseRight;
            mouse4Box.Checked = frame.Mouse4;
            mouse5Box.Checked = frame.Mouse5;
        }

        private void SaveToFile(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "TAS Input Files|*.tas|All Files|*.*";
            DialogResult res = dialog.ShowDialog();

            if (res != DialogResult.OK)
            {
                return;
            }

            string file = dialog.FileName;
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            SaveValues(frameBox.SelectedIndex);

            using FileStream stream = File.OpenWrite(file);
            using BinaryWriter writer = new BinaryWriter(stream);

            foreach (InputFrame frame in _frames)
            {
                writer.Write(frame.Length);
                writer.Write((byte)frame.Keys.Length);
                foreach (XnaKeys key in frame.Keys)
                {
                    writer.Write((byte)key);
                }

                writer.Write(frame.MouseX);
                writer.Write(frame.MouseY);
                writer.Write(frame.MouseScroll);

                byte mouseButtons = (byte)(frame.MouseLeft ? 1 : 0);
                mouseButtons |= (byte)((byte)(frame.MouseMiddle ? 1 : 0) << 1);
                mouseButtons |= (byte)((byte)(frame.MouseRight ? 1 : 0) << 2);
                mouseButtons |= (byte)((byte)(frame.Mouse4 ? 1 : 0) << 3);
                mouseButtons |= (byte)((byte)(frame.Mouse5 ? 1 : 0) << 4);
                writer.Write(mouseButtons);

                for (int i = 0; i < 4; i++)
                {
                    writer.Write(false);
                }
            }
        }

        private void LoadFromFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "TAS Input Files|*.tas|All Files|*.*";
            DialogResult res = dialog.ShowDialog();

            if (res != DialogResult.OK)
            {
                return;
            }

            string file = dialog.FileName;
            if (!File.Exists(file))
            {
                return;
            }

            using FileStream stream = File.OpenRead(file);
            using BinaryReader reader = new BinaryReader(stream);

            _frames.Clear();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                InputFrame frame = DeserializeFrame(reader);
                _frames.Add(frame);
            }

            UpdateFrameBox();

            _lastIndex = -1;
            frameBox.SelectedIndex = 0;
        }

        private InputFrame DeserializeFrame(BinaryReader reader)
        {
            int frames = reader.ReadInt32();

            // Keyboard
            byte keyLength = reader.ReadByte();
            XnaKeys[] keys = new XnaKeys[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                keys[i] = (XnaKeys)reader.ReadByte();
            }

            // Mouse
            int mouseX = reader.ReadInt32();
            int mouseY = reader.ReadInt32();
            int mouseScroll = reader.ReadInt32();
            byte mouseButtons = reader.ReadByte();

            bool leftButton = (mouseButtons & 0b00001) != 0;
            bool middleButton = (mouseButtons & 0b00010) != 0;
            bool rightButton = (mouseButtons & 0b00100) != 0;
            bool xButton1 = (mouseButtons & 0b01000) != 0;
            bool xButton2 = (mouseButtons & 0b10000) != 0;

            // Gamepads
            for (int i = 0; i < 4; i++)
            {
                bool connected = reader.ReadBoolean();
                if (!connected)
                {
                    continue;
                }

                reader.ReadUInt16();
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadInt16();
                reader.ReadInt16();
                reader.ReadInt16();
                reader.ReadInt16();
            }

            // Create frame
            InputFrame frame = new InputFrame(frames);
            frame.Keys = keys;
            frame.MouseX = mouseX;
            frame.MouseY = mouseY;
            frame.MouseScroll = mouseScroll;
            frame.MouseLeft = leftButton;
            frame.MouseMiddle = middleButton;
            frame.MouseRight = rightButton;
            frame.Mouse4 = xButton1;
            frame.Mouse5 = xButton2;

            return frame;
        }

        private void addKeyButton_Click(object sender, EventArgs e)
        {
            PressAnyKey keySelect = new PressAnyKey();
            DialogResult res = keySelect.ShowDialog();
            if (res == DialogResult.Cancel
                || keysBox.FindStringExact(keySelect.Key.ToString()) != ListBox.NoMatches)
            {
                return;
            }

            keysBox.Items.Add(keySelect.Key.ToString());
        }

        private void removeKeyButton_Click(object sender, EventArgs e)
        {
            int item = keysBox.SelectedIndex;
            if (item == -1)
            {
                return;
            }

            keysBox.Items.RemoveAt(item);
        }

        private int _lastIndex = -1;
        private void frameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveValues(_lastIndex);
            SetValues(frameBox.SelectedIndex);

            _lastIndex = frameBox.SelectedIndex;
        }

        private void addFrameButton_Click(object sender, EventArgs e)
        {
            _frames.Add(new InputFrame(1));
            UpdateFrameBox();

            frameBox.SelectedIndex = _frames.Count - 1;
        }

        private void removeFrameButton_Click(object sender, EventArgs e)
        {
            if (frameBox.SelectedIndex == -1 || _frames.Count == 1)
            {
                return;
            }

            int sel = frameBox.SelectedIndex;

            _frames.RemoveAt(sel);
            UpdateFrameBox();

            if (sel >= _frames.Count)
            {
                sel--;
            }

            frameBox.SelectedIndex = sel;
        }
    }
}
