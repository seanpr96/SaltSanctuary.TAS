using System;
using System.Windows.Forms;

namespace InputFileEditor
{
    public partial class ControllerConfig : UserControl
    {
        public bool Connected
        {
            get => connectedBox.Checked;
            set => connectedBox.Checked = value;
        }

        public byte LeftTrigger
        {
            get => (byte)leftTriggerUpDown.Value;
            set => leftTriggerUpDown.Value = value;
        }

        public byte RightTrigger
        {
            get => (byte)rightTriggerUpDown.Value;
            set => rightTriggerUpDown.Value = value;
        }

        public short LX
        {
            get => (short)lxUpDown.Value;
            set => lxUpDown.Value = value;
        }

        public short LY
        {
            get => (short)lyUpDown.Value;
            set => lyUpDown.Value = value;
        }

        public short RX
        {
            get => (short)rxUpDown.Value;
            set => rxUpDown.Value = value;
        }

        public short RY
        {
            get => (short)ryUpDown.Value;
            set => ryUpDown.Value = value;
        }

        public bool DpadUp
        {
            get => dpadUpBox.Checked;
            set => dpadUpBox.Checked = value;
        }

        public bool DpadDown
        {
            get => dpadDownBox.Checked;
            set => dpadDownBox.Checked = value;
        }

        public bool DpadLeft
        {
            get => dpadLeftBox.Checked;
            set => dpadLeftBox.Checked = value;
        }

        public bool DpadRight
        {
            get => dpadRightBox.Checked;
            set => dpadRightBox.Checked = value;
        }

        public bool Start
        {
            get => startBox.Checked;
            set => startBox.Checked = value;
        }

        public bool Back
        {
            get => backBox.Checked;
            set => backBox.Checked = value;
        }

        public bool RS
        {
            get => rsBox.Checked;
            set => rsBox.Checked = value;
        }

        public bool LS
        {
            get => lsBox.Checked;
            set => lsBox.Checked = value;
        }

        public bool LB
        {
            get => lbBox.Checked;
            set => lbBox.Checked = value;
        }

        public bool RB
        {
            get => rbBox.Checked;
            set => rbBox.Checked = value;
        }

        public bool BigButton
        {
            get => bigButtonBox.Checked;
            set => bigButtonBox.Checked = value;
        }

        public bool A
        {
            get => aBox.Checked;
            set => aBox.Checked = value;
        }

        public bool B
        {
            get => bBox.Checked;
            set => bBox.Checked = value;
        }

        public bool X
        {
            get => xBox.Checked;
            set => xBox.Checked = value;
        }

        public bool Y
        {
            get => yBox.Checked;
            set => yBox.Checked = value;
        }

        public ControllerConfig()
        {
            InitializeComponent();
        }

        public ushort GetButtons()
        {
            ButtonValues buttons = 0;
            buttons |= DpadUp ? ButtonValues.Up : 0;
            buttons |= DpadDown ? ButtonValues.Down : 0;
            buttons |= DpadLeft ? ButtonValues.Left : 0;
            buttons |= DpadRight ? ButtonValues.Right : 0;
            buttons |= Start ? ButtonValues.Start : 0;
            buttons |= Back ? ButtonValues.Back : 0;
            buttons |= RS ? ButtonValues.RightThumb : 0;
            buttons |= LS ? ButtonValues.LeftThumb : 0;
            buttons |= LB ? ButtonValues.LeftShoulder : 0;
            buttons |= RB ? ButtonValues.RightShoulder : 0;
            buttons |= BigButton ? ButtonValues.BigButton : 0;
            buttons |= A ? ButtonValues.A : 0;
            buttons |= B ? ButtonValues.B : 0;
            buttons |= X ? ButtonValues.X : 0;
            buttons |= Y ? ButtonValues.Y : 0;

            return (ushort)buttons;
        }

        public void SetButtons(ushort buttonsShort)
        {
            ButtonValues buttons = (ButtonValues)buttonsShort;

            DpadUp = (buttons & ButtonValues.Up) != 0;
            DpadDown = (buttons & ButtonValues.Down) != 0;
            DpadLeft = (buttons & ButtonValues.Left) != 0;
            DpadRight = (buttons & ButtonValues.Right) != 0;
            Start = (buttons & ButtonValues.Start) != 0;
            Back = (buttons & ButtonValues.Back) != 0;
            RS = (buttons & ButtonValues.RightThumb) != 0;
            LS = (buttons & ButtonValues.LeftThumb) != 0;
            LB = (buttons & ButtonValues.LeftShoulder) != 0;
            RB = (buttons & ButtonValues.RightShoulder) != 0;
            BigButton = (buttons & ButtonValues.BigButton) != 0;
            A = (buttons & ButtonValues.A) != 0;
            B = (buttons & ButtonValues.B) != 0;
            X = (buttons & ButtonValues.X) != 0;
            Y = (buttons & ButtonValues.Y) != 0;
        }

        private void connectedBox_CheckedChanged(object sender, EventArgs e)
        {
            leftTriggerUpDown.Enabled = connectedBox.Checked;
            rightTriggerUpDown.Enabled = connectedBox.Checked;
            lxUpDown.Enabled = connectedBox.Checked;
            lyUpDown.Enabled = connectedBox.Checked;
            rxUpDown.Enabled = connectedBox.Checked;
            ryUpDown.Enabled = connectedBox.Checked;

            dpadUpBox.Enabled = connectedBox.Checked;
            dpadDownBox.Enabled = connectedBox.Checked;
            dpadLeftBox.Enabled = connectedBox.Checked;
            dpadRightBox.Enabled = connectedBox.Checked;
            startBox.Enabled = connectedBox.Checked;
            backBox.Enabled = connectedBox.Checked;
            rsBox.Enabled = connectedBox.Checked;
            lsBox.Enabled = connectedBox.Checked;
            lbBox.Enabled = connectedBox.Checked;
            rbBox.Enabled = connectedBox.Checked;
            bigButtonBox.Enabled = connectedBox.Checked;
            aBox.Enabled = connectedBox.Checked;
            bBox.Enabled = connectedBox.Checked;
            xBox.Enabled = connectedBox.Checked;
            yBox.Enabled = connectedBox.Checked;
        }
    }
}
