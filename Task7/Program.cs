using System;
using System.Collections.Generic;

namespace Task7
{
    internal class Program
    {
        static void Main(string[] args)
        {            
           FileHandler productsFile = new FileHandler("../../../Products.txt");
           Storage storage = new Storage(productsFile);
           Console.WriteLine(storage);
        }
    }
}
