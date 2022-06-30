using System;
using Task9.Exceptions;
using Task9.FIleHandler;
using Task9.Models;
using Task9.Services;

namespace Task9
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.InputEncoding = System.Text.Encoding.Unicode;

                FileHandler<MenuModel> menuData = new("../../../Files/Menu.txt");
                menuData.ReadToObject(out MenuModel menu, StreamReadersService.MenuReader, null);
                Console.WriteLine(menu);

                FileHandler<PriceKurantModel> priceFile = new("../../../Files/Prices.txt");
                priceFile.ReadToObject(out PriceKurantModel priceKurant, StreamReadersService.PriceKurantReader, ValidatorsService.PriceKurantValidator);
                Console.WriteLine(priceKurant);

                FileHandler<ChangerModel> courseFile = new("../../../Files/Course.txt");
                courseFile.ReadToObject(out ChangerModel changer, StreamReadersService.ChangerReader, ValidatorsService.ChangerValidator);

                Console.WriteLine("Вага продуктів у всіх стравах: ");
                foreach (System.Collections.Generic.KeyValuePair<string, double> item in MenuService.GetIngridientsWeight(menu))
                {
                    Console.WriteLine(item.Key + " " + item.Value);
                }

                if (MenuService.TryGetMenuTotalSum(menu, priceKurant, out double price, ConsoleSetNewProduct) == false)
                {
                    Console.WriteLine("\nНе вдалося отримати ціну меню.");
                    return;
                }
                priceFile.WriteToFile(priceKurant);

                Console.WriteLine("\nЦіна всіх страв у меню:");
                Console.WriteLine($"Ціна у грн: {price}");
                Console.WriteLine("У яку валюту конвертувати ?");
                Console.WriteLine(changer);
                Console.Write("введіть назву валюти > ");
                if (ChangerService.TryConvert(Console.ReadLine(), price, changer, out double result))
                {
                    Console.WriteLine($"Результат конвертації > {result}");
                }
                else
                {
                    Console.WriteLine("Конвертація невдала");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        public static bool ConsoleSetNewProduct(ref PriceKurantModel priceKurant, string name)
        {
            Console.WriteLine($"\nЦіну продукта {name} не знайдено");
            Console.Write($"Введіть ціну продукта: {name} > ");
            if (double.TryParse(Console.ReadLine(), out double price) == false)
            {
                Console.Write("Хибний формат ціни. \nСпробувати ще раз > 1 \nНе додавати > будь яка кнопка \nВведіть > ");
                if (Console.ReadKey().Key is not ConsoleKey.D1)
                {
                    throw new ProductNotFoundException($"Продукт {name} не знайдено");
                }
                return false;
            }
            priceKurant.Add(name, price);
            return true;

        }
    }
}
