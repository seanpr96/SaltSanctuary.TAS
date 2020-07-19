using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace TAS.Utils
{
    public static class KeyboardStateExtensions
    {
        public static KeyboardState RemoveHotkeys(this KeyboardState self)
        {
            List<Keys> keys = new List<Keys>();

            foreach (Keys key in self.GetPressedKeys())
            {
                if (!HotkeyHandler.IsHotkey(key))
                {
                    keys.Add(key);
                }
            }

            return new KeyboardState(keys.ToArray());
        }
    }
}
