using System;
using System.Collections.Generic;

namespace Task11.ConsoleUI.ConsoleProductReaders
{
    internal abstract class FoodProductConsoleReaderBehaviorBase : ProductConsoleReaderBehaviorBase, IConsoleProductReader<IFoodProduct>
    {
        public abstract override IFoodProduct ConsoleReadProduct();
        protected virtual void ConsoleReadFoodProductBase<T>(ref T product)
           where T : class, IFoodProduct
        {
            ConsoleReadProductBase(ref product);
            ConsoleReadWeight(ref product);
            ConsoleReadExpirationTime(ref product);
            ConsoleReadDaysToExpirationAndPresentOfChange(ref product);
        }
        protected virtual void ConsoleReadWeight<T>(ref T product)
           where T : class, IFoodProduct
        {
            Console.Write("Введіть вагу: ");
            string weightLine = Console.ReadLine();
            if (double.TryParse(weightLine, out double weighResult) == false)
            {
                throw new ArgumentException("Хибний формат ваги");
            }
            product.Weight = weighResult;
        }
        protected virtual void ConsoleReadExpirationTime<T>(ref T product)
           where T : class, IFoodProduct
        {
            Console.Write("Введіть дату терміну придатності: ");
            string expirationTimeLine = Console.ReadLine();
            if (DateTime.TryParse(expirationTimeLine, out DateTime expirationTimeResult) == false)
            {
                throw new ArgumentException("Хибний формат терміну придатності");
            }
            product.ExpirationTime = expirationTimeResult;
        }
        protected virtual void ConsoleReadDaysToExpirationAndPresentOfChange<T>(ref T product)
          where T : class, IFoodProduct
        {
            SortedDictionary<int, int> daysToExpirationAndPresentOfChange = new();
            bool isAddNewPair = true;
            Console.WriteLine("Додати зміну ціни за кількістью днів до терміну придатності ?");
            do
            {
                Console.WriteLine("0 -> так");
                Console.WriteLine("1 -> ні");
                Console.Write("Оберіть варіант > ");
                if (Console.ReadLine() == "0")
                {
                    Console.Write("Введіть кількість днів до терміну придатності: ");
                    string daysLine = Console.ReadLine();
                    if (int.TryParse(daysLine, out int daysResult) == false)
                    {
                        throw new ArgumentException("Хибний кількості днів до терміну придатності");
                    }
                    Console.Write("Введіть на скільки відсотків змінити ціну: ");
                    string presentLine = Console.ReadLine();
                    if (int.TryParse(presentLine, out int presentResult) == false)
                    {
                        throw new ArgumentException("Хибний формат відсотків");
                    }
                    if (daysToExpirationAndPresentOfChange.TryAdd(daysResult, presentResult) == false)
                    {
                        Console.WriteLine("Вже існує зміна ціни для цієї кількості днів");
                    }
                    Console.WriteLine("Додати ще один елемент ?");
                }
                else
                {
                    isAddNewPair = false;
                }

            } while (isAddNewPair);
            product.DaysToExpirationAndPresentOfChange = daysToExpirationAndPresentOfChange;
        }
    }
}
