using System;

namespace Task11.ConsoleUI.ConsoleProductReaders
{
    internal abstract class ProductConsoleReaderBehaviorBase : IConsoleProductReader<IProduct>
    {
        public abstract IProduct ConsoleReadProduct();
        protected virtual void ConsoleReadProductBase<T>(ref T product)
            where T : class, IProduct
        {
            ConsoleReadProductName(ref product);
            ConsoleReadProductPrice(ref product);
        }

        protected virtual void ConsoleReadProductName<T>(ref T product)
            where T : class, IProduct
        {
            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Відсутне ім'я");
            }
            product.Name = name;
        }
        protected virtual void ConsoleReadProductPrice<T>(ref T product)
            where T : class, IProduct
        {
            Console.Write("Введіть ціну: ");
            string priceLine = Console.ReadLine();
            if (double.TryParse(priceLine, out double priceResult) == false)
            {
                throw new ArgumentException("Хибний формат ціни");
            }
            product.Price = priceResult;
        }
    }
}
