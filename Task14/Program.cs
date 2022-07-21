using Task14;
using Task14.ConsoleUI;
using Task14.ConsoleUI.ConsoleProductReaders;
using Task14.Parsers;
using Task14.Product;
using Task14.Product.MovieProduct;
using Task14.Services;
using Task14.Validators;
using Task14.Serialize;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

try
{
    Logger.Instance.Path = "../../../Files/Logs.txt";
    
    Dictionary<string, ITXTSerializedParametersParser<IProduct>> ParsersByType = new()
    {
        { new DairyProductModel().GetType().Name, new DairyProductParser(Logger.Instance.Log) },
        { new MovieProductModel().GetType().Name, new MovieProductParser(Logger.Instance.Log) },
        { new MeatProductModel().GetType().Name, new MeatProductParser(Logger.Instance.Log) }
    };

    Dictionary<string, IConsoleProductReader<IProduct>> ConsoleReaderByType = new()
    {
        { "Молочний продукт", new DairyProductConsoleReaderBehavior() },
        { "М'ясний продукт", new MeatProductConsoleReaderBehavior() },
        { "Фільм", new MovieProductConsoleReaderBehavior() }
    };
    ProductStorage<IProduct>.Instance.OnProductPreAddFaceControl += PreAddBehaviorService.IsProductNotExpired;
    ProductStorage<IProduct>.Instance.OnBadProductLogger += Logger.Instance.Log;

    ConsoleProductStorageProcessor<IProduct> consoleProductStorageProcessor = new(ProductStorage<IProduct>.Instance, ConsoleReaderByType, ParsersByType);
    consoleProductStorageProcessor.StreamStorageSerializer = new XmlStreamSerializer<ProductStorage<IProduct>>();
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
