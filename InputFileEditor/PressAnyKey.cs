using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

namespace InputFileEditor
{
    public partial class PressAnyKey : Form
    {
        private KeyboardState _kb;
        private KeyboardState _oldKb;

        public XnaKeys Key { get; private set; } = XnaKeys.None;

        public PressAnyKey()
        {
            InitializeComponent();
            Application.Idle += CheckKeys;
            _kb = Keyboard.GetState();
            _oldKb = _kb;
        }

        private void CheckKeys(object sender, EventArgs e)
        {
            _kb = Keyboard.GetState();

            foreach (XnaKeys pressedKey in _kb.GetPressedKeys())
            {
                if (!_oldKb.IsKeyDown(pressedKey))
                {
                    Key = pressedKey;
                    Application.Idle -= CheckKeys;
                    DialogResult = DialogResult.OK;
                    break;
                }
            }

            _oldKb = _kb;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
