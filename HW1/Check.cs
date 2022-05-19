using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1
{
    internal static class Check
    {
        static int _checkCounter = 1;
        public static void PrintCheck(Buy buy)
        {
            Console.WriteLine();
            Console.WriteLine($"<------------------ Check {_checkCounter} ------------------>");
            foreach (var product in buy.ProductList)           
                Console.WriteLine(product);            
            Console.WriteLine($"Total weight: {buy.ProductWeightSum}");
            Console.WriteLine($"Total Price: {buy.ProductPriceSum}");
            _checkCounter++;
        }
    }
}
