using System.IO;
using System.Text;

namespace TAS.Utils
{
    public static class Logger
    {
        private static readonly object Locker = new object();
        private static readonly StreamWriter Writer;

        static Logger()
        {
            FileStream fs = new FileStream("taslog.txt", FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            Writer = new StreamWriter(fs, Encoding.UTF8) { AutoFlush = true };
        }

        /// <summary>
        /// Logs given messages to the file "taslog.txt"
        /// </summary>
        public static void Log(string text)
        {
            lock (Locker)
            {
                Writer.WriteLine(text);
            }
        }

        /// <summary>
        /// Logs given messages to the file "taslog.txt"
        /// </summary>
        public static void Log(object text) => Log(text?.ToString() ?? "null");
    }
}
