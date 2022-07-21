namespace Task14.ConsoleUI.ConsoleProductReaders
{
    internal interface IConsoleProductReader<out T>
        where T : IProduct
    {
        T ConsoleReadProduct();
    }
}
