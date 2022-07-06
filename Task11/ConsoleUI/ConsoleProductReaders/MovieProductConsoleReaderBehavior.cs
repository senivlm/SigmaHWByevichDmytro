using System;
using Task11.Product.General;
using Task11.Product.MovieProduct;

namespace Task11.ConsoleUI.ConsoleProductReaders
{
    internal class MovieProductConsoleReaderBehavior : ProductConsoleReaderBehaviorBase, IConsoleProductReader<IMovieProduct>
    {
        public override IMovieProduct ConsoleReadProduct()
        {
            try
            {
                IMovieProduct product = new MovieProductModel();
                ConsoleReadProductBase(ref product);
                ConsoleReadGenre(ref product);
                ConsoleReadDuration(ref product);
                ConsoleReadLink(ref product);
                ConsoleReadAuthorName(ref product);
                return product;
            }
            catch (Exception)
            {

                throw;
            }

        }
        private void ConsoleReadGenre<T>(ref T product)
          where T : class, IMovieProduct
        {
            Console.Write("Введіть жанр: ");
            string genreLine = Console.ReadLine();
            if (string.IsNullOrEmpty(genreLine.Trim()))
            {
                throw new ArgumentException("Відсутній жанр");
            }
            product.Genre = genreLine;
        }
        private void ConsoleReadDuration<T>(ref T product)
          where T : class, IMovieProduct
        {
            Console.Write("Введіть тривалість фільму: ");
            string durationLine = Console.ReadLine();
            if (TimeSpan.TryParse(durationLine, out TimeSpan durationResut) == false)
            {
                throw new ArgumentException("Хибний формат тривалості");
            }
            product.Duration = durationResut;
        }
        private void ConsoleReadLink<T>(ref T product)
          where T : class, IMovieProduct
        {
            Console.Write("Введіть посилання: ");
            string linkLine = Console.ReadLine();
            if (Uri.IsWellFormedUriString(linkLine, UriKind.Absolute) == false)
            {
                throw new ArgumentException("Хибний формат посилання; ");
            }
            product.Link = linkLine;
        }
        private void ConsoleReadAuthorName<T>(ref T product)
         where T : class, IMovieProduct
        {
            Console.Write("Введіть ім'я автора: ");
            string authorNameLine = Console.ReadLine();
            if (string.IsNullOrEmpty(authorNameLine.Trim()))
            {
                throw new ArgumentException("Відсутніе ім'я автора");
            }
            product.Link = authorNameLine.Trim();
        }
    }
}
