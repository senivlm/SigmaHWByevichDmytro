using System;
using System.Collections.Generic;
using Task11.Enums;
using Task11.Product;

namespace Task11
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            try
            {
                ProductStorage<IProduct> productStorage = new();
                SortedDictionary<int, int> daysToExpirationAndPresentOfChange = new SortedDictionary<int, int>
                {
                    { 6, 25 },
                    { 8, 10 },
                    { 3, 40 },
                    { 7, 20 }
                };
                IProduct meat = new MeatProductModel("name1", 20, 1, DateTime.Today.AddDays(4), MeatSpecies.VEAL, MeatCategory.THIRD, daysToExpirationAndPresentOfChange);
                IProduct meat2 = new MeatProductModel("name2", 24, 3, DateTime.Today.AddDays(4), MeatSpecies.PORK, MeatCategory.SECOND, daysToExpirationAndPresentOfChange);
                IProduct meat3 = new MeatProductModel("name3", 21, 2, DateTime.Today.AddDays(2), MeatSpecies.CHIKEN, MeatCategory.FIRST, daysToExpirationAndPresentOfChange);
                IProduct diary = new DairyProductModel("name4", 40, 2, DateTime.Today.AddDays(0), daysToExpirationAndPresentOfChange);
                productStorage.Add(meat);
                productStorage.Add(meat2);
                productStorage.Add(meat3);
                productStorage.Add(diary);
                foreach (var item in productStorage.GetAll<IMeatProduct>())
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }


        }
    }
}
