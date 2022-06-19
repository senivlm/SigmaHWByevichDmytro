using System;
using System.Collections.Generic;

namespace Task8_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product("p1", 30, 10);
            Product product2 = new Product("p2", 31, 10);
            Meat meat1 = new Meat("m1", 20, 3, MeatSpecies.Mutton, MeatCategory.First);
            Meat meat2 = new Meat("m2", 21, 2, MeatSpecies.Chicken, MeatCategory.Second);
            DairyProduct dairyProduct1 = new DairyProduct("d1", 10, 5, 13);
            DairyProduct dairyProduct2 = new DairyProduct("d2", 15, 1, 12);

            Storage storage1 = new StorageBuilder().AddProduct(product1)
                                                  .AddProduct(product2)
                                                  .AddProduct(meat1)
                                                  .AddProduct(dairyProduct2)
                                                  .Build();

            Storage storage2 = new StorageBuilder().AddProduct(product1)
                                                  .AddProduct(product2)
                                                  .AddProduct(meat2)
                                                  .AddProduct(dairyProduct1)
                                                  .Build();
            var subtract = storage1.Subtract(storage2);
            var intersection = storage1.Intersection(storage2);
            var union = storage1.Union(storage2);
            Console.WriteLine("storage 1:");
            Console.WriteLine(storage1);
            Console.WriteLine("storage 2:");
            Console.WriteLine(storage2);

            Console.WriteLine("\nsubtract");
            foreach (var item in subtract)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nintersection");
            foreach (var item in intersection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nunion");
            foreach (var item in union)
            {
                Console.WriteLine(item);
            }
        }

    }

}
