using System;
using System.Collections.Generic;
using Task11.Enums;
using Task11.FileHandler;
using Task11.Product;
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
            try
            {
                Dictionary<string, IStringParser<IProduct>> ParsersByType = new()
                {
                    { "DairyProduct", new DairyProductParser() }
                };


                FileHandlerService.ReadToCollection(
                    out ProductStorage<IProduct> storage,
                    new TXTSerializedStorageReader<IProduct>(),
                    ParsersByType, "../../../Files/ProductsData.txt");
                Console.WriteLine(storage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }


        }
    }
}
