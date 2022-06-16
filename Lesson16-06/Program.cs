using System;
using System.Collections.Generic;
using System.IO;

namespace Lesson16_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            try
            {
                Dictionary<string, string> dictionary = TextReader.ReadDictionary("../../../Dictionary.txt");
                IEnumerable<string> text = TextReader.ReadText("../../../Text.txt");

                Translator translator = new Translator();
                translator.SetPath("../../../Dictionary.txt");

                translator.SetDictionary(dictionary);

                foreach (string line in text)
                {
                    translator.AddText(line);
                }

                Console.WriteLine(translator.TranslateWords());

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
