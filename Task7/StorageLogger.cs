using System;
using System.IO;

namespace Task7
{
    internal static class StorageLogger
    {
        public static string Path { get; set; }
        private static bool IsPathIntroduced()
        {
            return Path is not null;
        }
        public static void LogAppend(string logLine)
        {
            if (!IsPathIntroduced())
            {
                throw new NullReferenceException();
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(Path, true))
                {
                    writer.WriteLine(logLine + $" {DateTime.Now:g}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
