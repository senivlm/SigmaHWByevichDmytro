using System;

namespace Task8_3
{
    internal static class Check
    {
        private static int _checkCounter = 1;
        public static void PrintCheck(Buy buy)
        {
            buy.UpdateProductPriceSum();
            buy.UpdateProductWeightSum();
            Console.WriteLine();
            Console.WriteLine($"<------------------ Check {_checkCounter} ------------------>");
            foreach (Product product in buy.ProductList)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine($"Total weight: {buy.ProductWeightSum}");
            Console.WriteLine($"Total Price: {buy.ProductPriceSum}");
            _checkCounter++;
        }
    }
}
