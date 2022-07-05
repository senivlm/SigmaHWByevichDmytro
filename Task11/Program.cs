using System;
using System.Collections.Generic;
using Task11.FileHandler;
using Task11.Parsers;
using Task11.Product;
using Task11.Readers;
using Task11.Services;
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
                Logger.Instance.Path = "../../../Files/Logs.txt";

                Dictionary<string, IStringParser<IProduct>> ParsersByType = new()
                {
                    { "DairyProduct", new DairyProductParser(Logger.Instance.Log) },
                    { "MovieProduct", new MovieProductParser(Logger.Instance.Log) },
                    { "MeatProduct", new MeatProductParser(Logger.Instance.Log) }
                };

                ProductStorage<IProduct> storage = new ProductStorage<IProduct>();
                storage.OnProductPreAddFaceControl += PreAddBehaviorService.IsProductNotExpired;
                storage.OnBadProductLogger += Logger.Instance.Log;

                FileHandlerService.ReadToCollection
                (
                    obj: ref storage,
                    collectionReader: new TXTSerializedStorageReader<IProduct>(Logger.Instance.Log),
                    parser: ParsersByType,
                    path: "../../../Files/ProductsData.txt"
                );

                FileHandlerService.WriteToFile(storage, "../../../Files/ProductsData.txt");
                storage.Sort();
                foreach (var product in storage.GetAll<IDairyProduct>(product => product.Price > 10))
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine(storage);
                Console.WriteLine($"Price: {storage.Pirice}");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine($"Програма заверешeна, у логи було занесено: {Logger.Instance.ExCount} запис(ів)");
            }
        }

    }
}
