using System;
using System.Collections.Generic;

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

            var options = new List<Option>()
            {
               new Option("Зчитати дані з файлу", () => flatsDataFile.ReadObject(flats)),
               new Option("Записати звіт у файл", () => reportFile.WriteObject(flats))
            };
            Menu menu = new Menu(options);
            menu.PrintMenu();
        }
    }
}
