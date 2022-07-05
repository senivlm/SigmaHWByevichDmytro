using System;
using Task11.Product.General;
using Task11.Product.MovieProduct;

namespace Task11.ConsoleUI.ConsoleProductAdders
{
    internal class MovieProductConsoleReaderBehavior : IConsoleProductReader<IMovieProduct>
    {
        public IMovieProduct ConsoleReadProduct()
        {
            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name.Trim()))
            {
                throw new ArgumentNullException("Відсутне ім'я");
            }

            Console.Write("Введіть ціну: ");
            string priceLine = Console.ReadLine();
            if (double.TryParse(priceLine, out double priceResult) == false)
            {
                throw new ArgumentException("Хибний формат ціни");
            }

            Console.Write("Введіть жанр: ");
            string genreLine = Console.ReadLine();
            if (string.IsNullOrEmpty(genreLine.Trim()))
            {
                throw new ArgumentException("Відсутній жанр");
            }

            Console.Write("Введіть тривалість фільму: ");
            string durationLine = Console.ReadLine();
            if (TimeSpan.TryParse(durationLine, out TimeSpan durationResut) == false)
            {
                throw new ArgumentException("Хибний формат тривалості");
            }
            Console.Write("Введіть посилання: ");
            string linkLine = Console.ReadLine();
            if (Uri.IsWellFormedUriString(linkLine, UriKind.Absolute) == false)
            {
                throw new ArgumentException("Хибний формат посилання; ");
            }
            Console.Write("Введіть ім'я автора: ");
            string authorNameLine = Console.ReadLine();
            if (string.IsNullOrEmpty(authorNameLine.Trim()))
            {
                throw new ArgumentException("Відсутніе ім'я автора");
            }

            return new MovieProductModel(name, priceResult, genreLine, durationResut, linkLine, authorNameLine);
        }
    }
}
