using System;

namespace Task6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            //DateTime[] dateTimes = new DateTime[4]
            //{
            //    DateTime.Parse("01.9.2021"),
            //    DateTime.Parse("02.10.2021"),
            //    DateTime.Parse("01.11.2021"),
            //    DateTime.Parse("03.12.2021")
            //};
            //FlatModel model = new FlatModel(4, "Nabok", 314.1231, 375.2141, dateTimes);
            //Console.WriteLine(model);
            Flats flats = new Flats("../../../FlatsInfo.txt");
            Console.WriteLine(flats);
        }
    }
}
