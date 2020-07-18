using System.Threading;
using Microsoft.Xna.Framework.Input;
using TAS.Patches.ProjectTower;

namespace TAS
{
    /// <summary>
    /// Handles all TAS hotkeys
    /// </summary>
    public static class HotkeyHandler
    {
        private static KeyboardState _kb;
        private static KeyboardState _oldKb;

        private static Thread _infoThread;

        public static void Initialize()
        {
            Game1.Instance.OnTick += CheckButtons;
        }

        private static void CheckButtons()
        {
            _kb = InputManager.GetActualKeyboard();

            if (WasPressed(Config.FastForwardKey))
            {
                Game1.Instance.FastForward = !Game1.Instance.FastForward;
                Game1.Instance.AdvanceClock();
            }

            if (WasPressed(Config.PauseKey))
            {
                Game1.Instance.Paused = !Game1.Instance.Paused;
                Game1.Instance.AdvanceClock();
            }

            if (WasPressed(Config.FrameAdvanceKey))
            {
                Game1.Instance.AdvanceFrame();
            }

            if (WasPressed(Config.ShowInfoKey))
            {
                if (_infoThread != null && _infoThread.IsAlive)
                {
                    InfoWindow.NeedsClose = true;
                }
                else
                {
                    _infoThread = new Thread(() => System.Windows.Forms.Application.Run(new InfoWindow()));
                    _infoThread.Start();
                }
            }

            _oldKb = _kb;
        }

        private static bool WasPressed(Keys key) => _kb.IsKeyDown(key) && !_oldKb.IsKeyDown(key);
    }
}
