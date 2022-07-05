using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Enums;
using Task11.Product;

namespace Task11.ConsoleUI.ConsoleProductAdders
{
    internal class MeatProductConsoleReaderBehavior : IConsoleProductReader
    {
        public IProduct ConsoleReadProduct()
        {
            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Відсутне ім'я");
            }

            Console.Write("Введіть ціну: ");
            string priceLine = Console.ReadLine();
            if (double.TryParse(priceLine, out double priceResult) == false)
            {
                throw new ArgumentException("Хибний формат ціни");
            }

            Console.Write("Введіть вагу: ");
            string weightLine = Console.ReadLine();
            if (double.TryParse(weightLine, out double weighResult) == false)
            {
                throw new ArgumentException("Хибний формат ваги");
            }

            Console.Write("Введіть дату терміну придатності: ");
            string expirationTimeLine = Console.ReadLine();
            if (DateTime.TryParse(expirationTimeLine, out DateTime expirationTimeResult) == false)
            {
                throw new ArgumentException("Хибний формат терміну придатності");
            }
            Console.Write("Введіть тип м'яса: ");
            string meatSpeciesLine = Console.ReadLine();
            if (Enum.TryParse(meatSpeciesLine, out MeatSpecies meatSpeciesResult) == false)
            {
                throw new ArgumentException("Хибний формат типу м'яса");
            }

            Console.Write("Введіть категорію м'яса: ");
            string meatCategoryLine = Console.ReadLine();
            if (Enum.TryParse(meatCategoryLine, out MeatCategory meatCategoryResult) == false)
            {
                throw new ArgumentException("Хибний формат категорії м'яса");
            }

            SortedDictionary<int, int> daysToExpirationAndPresentOfChange = new();
            bool isAddNewPair = true;
            Console.WriteLine("Додати зміну ціни за кількістью днів до терміну придатності ?");
            do
            {
                Console.WriteLine("0 -> так");
                Console.WriteLine("1 -> ні");
                Console.Write("Оберіть варіант > ");
                if (bool.Parse(Console.ReadLine()))
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
                }
                Console.WriteLine("Додати ще один елемент ?");
            } while (isAddNewPair);
            return new MeatProductModel(name, priceResult, weighResult, expirationTimeResult,meatSpeciesResult,meatCategoryResult, daysToExpirationAndPresentOfChange);
        }
    }
}
