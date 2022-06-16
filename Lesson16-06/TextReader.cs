using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson16_06
{
    internal class TextReader
    {
        public static IEnumerable<string> ReadText(string filePath)
        {
            List<string> text = new List<string>();
            using (StreamReader reader = new(filePath) )
            {
                while(!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
        public static void AddToDictionary(string filePath,string key, string value)
        {
            using (StreamWriter writer = new StreamWriter(filePath,true))
            {
                writer.Write('\n'+key + '-'+ value);
            }
        }
        public static Dictionary<string,string> ReadDictionary(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not founded");
            }
            Dictionary<string,string> result = new Dictionary<string,string>();
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string tmp = reader.ReadLine();
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        var str = tmp.Trim().Split('-');
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
