using XnaKeys = Microsoft.Xna.Framework.Input.Keys;

namespace InputFileEditor
{
    public struct InputFrame
    {
        public int Length;
        public XnaKeys[] Keys;
        public int MouseX;
        public int MouseY;
        public int MouseScroll;
        public bool MouseLeft;
        public bool MouseMiddle;
        public bool MouseRight;
        public bool Mouse4;
        public bool Mouse5;

        public InputFrame(int length)
        {
            this = default;

            if (length <= 0)
            {
                length = 1;
            }

            Length = length;
            Keys = new XnaKeys[0];
            MouseX = -256;
            MouseY = -256;
        }
    }
}
