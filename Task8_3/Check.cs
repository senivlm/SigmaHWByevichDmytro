using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_3
{
    internal static class Check
    {
        static int _checkCounter = 1;
        public static void PrintCheck(Buy buy)
        {
            buy.UpdateProductPriceSum();
            buy.UpdateProductWeightSum();
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
