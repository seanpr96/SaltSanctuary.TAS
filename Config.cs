using System.IO;
using Microsoft.Xna.Framework.Input;

namespace TAS
{
    /// <summary>
    /// Contains all of the user configuration for the TAS tools
    /// </summary>
    public static class Config
    {
        // TODO: Actually load config from somewhere
        static Config()
        {
            if (File.Exists("input.tas"))
            {
                InitialState = InputState.Playback;
            }
            else
            {
                InitialState = InputState.Recording;
            }
        }


        public static Keys FastForwardKey = Keys.NumPad0;
        public static Keys PauseKey = Keys.NumPad1;
        public static Keys FrameAdvanceKey = Keys.NumPad2;

        public static Keys ShowInfoKey = Keys.NumPad9;

        public static int Seed = 56;
        public static InputState InitialState = InputState.Normal;
        public static bool StartPaused = true;
    }
}
