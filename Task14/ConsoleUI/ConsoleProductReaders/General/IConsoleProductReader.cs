namespace Task11.ConsoleUI.ConsoleProductReaders
{
    internal interface IConsoleProductReader<out T>
        where T : IProduct
    {
        T ConsoleReadProduct();
    }
}
