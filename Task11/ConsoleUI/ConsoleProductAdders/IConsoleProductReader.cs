namespace Task11.ConsoleUI.ConsoleProductAdders
{
    internal interface IConsoleProductReader<out T>
        where T : IProduct
    {
        T ConsoleReadProduct();
    }
}
