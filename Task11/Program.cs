using System;
using System.Collections.Generic;
using Task11.Enums;
using Task11.FileHandler;
using Task11.Parsers;
using Task11.Product;
using Task11.Product._General;
using Task11.Product.General;
using Task11.Product.MovieProduct;
using Task11.Readers;
using Task11.Validators;

namespace Task11
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            //FileHandlerService.WriteToFile
            //    (
            //        obj: new MeatProductModel("Steak", 120, 3, DateTime.Today.AddDays(7), MeatSpecies.MUTTON, MeatCategory.FIRST, new SortedDictionary<int, int> { { 2, 40 }, { 5, 20 }, { 7, 10 } }),
            //        path: "../../../Files/ProductsData.txt",
            //        append: true
            //    );


            try
            {
                Dictionary<string, IStringParser<IProduct>> ParsersByType = new()
                {
                    { "DairyProduct", new DairyProductParser() },
                    { "MovieProduct", new MovieProductParser() },
                    { "MeatProduct", new MeatProductParser() }
                };


                FileHandlerService.ReadToCollection(
                    out ProductStorage<IProduct> storage,
                    new TXTSerializedStorageReader<IProduct>(),
                    ParsersByType,
                    "../../../Files/ProductsData.txt");

                foreach (var item in storage.GetAll<IProduct>())
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
