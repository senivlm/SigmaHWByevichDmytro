using System;
using System.IO;

namespace Task6_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader("../../../Text.txt"))
                {
                    TextHandler textHandler = new TextHandler(sr);
                    Console.WriteLine(textHandler.GetMaxMinWordsInSentences());
                    using (StreamWriter sw = new StreamWriter("../../../Result.txt"))
                    {
                        textHandler.WriteSentancesInFile(sw);
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
