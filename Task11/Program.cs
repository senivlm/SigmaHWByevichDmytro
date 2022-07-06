using System;
using System.Collections.Generic;
using Task11.ConsoleUI;
using Task11.ConsoleUI.ConsoleProductReaders;
using Task11.Parsers;
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

                Dictionary<string, ITXTSerializedParametersParser<IProduct>> ParsersByType = new()
                {
                    { "DairyProductModel", new DairyProductParser(Logger.Instance.Log) },
                    { "MovieProductModel", new MovieProductParser(Logger.Instance.Log) },
                    { "MeatProductModel", new MeatProductParser(Logger.Instance.Log) }
                };

                Dictionary<string, IConsoleProductReader<IProduct>> ConsoleReaderByType = new()
                {
                    { "Молочний продукт", new DairyProductConsoleReaderBehavior() },
                    { "М'ясний продукт", new MeatProductConsoleReaderBehavior() },
                    { "Фільм", new MovieProductConsoleReaderBehavior() }
                };

                ProductStorage<IProduct> storage = new();
                storage.OnProductPreAddFaceControl += PreAddBehaviorService.IsProductNotExpired;
                storage.OnBadProductLogger += Logger.Instance.Log;

                ConsoleProductStorageProcessor<IProduct> consoleProductStorageProcessor = new(storage, ConsoleReaderByType, ParsersByType);
                consoleProductStorageProcessor.PrintMenu();

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
