using System;
using System.Collections.Generic;

namespace Task8_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Product product1 = new Product("p1",30,10);
           Product product2 = new Product("p2",31,10);

            Console.WriteLine(product1.CompareTo(product1));
        }

    }

}
