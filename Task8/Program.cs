using System;
using System.Collections.Generic;

namespace Task8_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            FileHandler flatsDataFile = new FileHandler("../../../FlatsData.txt");
            FileHandler reportFile = new FileHandler("../../../Report.txt");


            List<FlatModel> flats1 = new List<FlatModel>()
            {
                new FlatModel(1,"Nabaka",100,200,new []{DateTime.Now.AddDays(1),DateTime.Now.AddDays(2),DateTime.Now.AddDays(3) }),
                new FlatModel(2,"Praka",110,180,new []{DateTime.Now.AddDays(4),DateTime.Now.AddDays(5),DateTime.Now.AddDays(6) }),
                new FlatModel(3,"Vlad",150,180,new []{DateTime.Now.AddDays(7),DateTime.Now.AddDays(8),DateTime.Now.AddDays(9) })
            };
            FlatsElectricityDebts flatsElectricityDebts1 = new FlatsElectricityDebts(flats1);


            List<FlatModel> flats2 = new List<FlatModel>()
            {
                new FlatModel(1,"Nabaka",100,200,new []{DateTime.Now.AddDays(1),DateTime.Now.AddDays(2),DateTime.Now.AddDays(3) }),
                new FlatModel(4,"Oleg",110,180,new []{DateTime.Now.AddDays(9),DateTime.Now.AddDays(8),DateTime.Now.AddDays(7) }),
                new FlatModel(5,"Dima",150,180,new []{DateTime.Now.AddDays(6),DateTime.Now.AddDays(5),DateTime.Now.AddDays(4) })
            };
            FlatsElectricityDebts flatsElectricityDebts2 = new FlatsElectricityDebts(flats2);

            Console.WriteLine(flatsElectricityDebts1 + flatsElectricityDebts2);
            Console.WriteLine();
            Console.WriteLine(flatsElectricityDebts1 - flatsElectricityDebts2);
        }
    }
}
