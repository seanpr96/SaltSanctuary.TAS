using System;

namespace InputFileEditor
{
    public struct ControllerState
    {
        public bool Connected;

        public ushort Buttons;
        public byte LeftTrigger;
        public byte RightTrigger;
        public short ThumbLX;
        public short ThumbLY;
        public short ThumbRX;
        public short ThumbRY;
    }

    [Flags]
    public enum ButtonValues : ushort
    {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        Start = 16,
        Back = 32,
        RightThumb = 128,
        LeftThumb = 64,
        LeftShoulder = 256,
        RightShoulder = 512,
        BigButton = 2048,
        A = 4096,
        B = 8192,
        X = 16384,
        Y = 32768,
    }
}
