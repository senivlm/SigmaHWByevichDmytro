using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_3
{
    internal static class StorageLogger
    {
        static public string Path { get; set; }
        static private bool IsPathIntroduced()
        {
            return Path is not null;
        }
        static public void LogAppend(string logLine)
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
