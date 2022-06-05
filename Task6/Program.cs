using System;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            FileHandler flatsDataFile = new FileHandler("../../../FlatsData.txt");
            FileHandler reportFile = new FileHandler("../../../Report.txt");

            Flats flats = new Flats();

            flatsDataFile.ReadObject(flats);
            reportFile.WriteObject(flats);

            try
            {
                Console.WriteLine(flats[0]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Невірний індекс");
            }
        }
    }
}
