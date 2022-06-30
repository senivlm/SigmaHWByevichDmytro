using System;
using System.Collections.Generic;
using System.IO;

namespace Task10
{
    internal class TextReader
    {
        public static IEnumerable<string> ReadTextByLine(string filePath)
        {
            List<string> text = new List<string>();
            using (StreamReader reader = new(filePath))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
        public static string ReadAllText(string filePath)
        {
            using (StreamReader reader = new(filePath))
            {
                return reader.ReadToEnd();
            }
        }
        public static void AddToDictionary(string filePath, string key, string value)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.Write('\n' + key + '-' + value);
            }
        }
        public static Dictionary<string, string> ReadDictionary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not founded");
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string tmp = reader.ReadLine();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        string[] str = tmp.Trim().Split(" - ");
                        if (str.Length != 2)
                        {
                            throw new ArgumentException("Incorrect dictionary format");
                        }
                        result.Add(str[0], str[1]);
                    }
                }
            }
            return result;
        }
    }
}
