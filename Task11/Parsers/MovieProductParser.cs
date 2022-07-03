using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task11.Product.General;
using Task11.Product.MovieProduct;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MovieProductParser : ProductParserBase, IStringParser<IMovieProduct>
    {
        public override IMovieProduct Parse(string str)
        {
            try
            {
                Regex namePattern = new(@"<(Name:) ([\w_\s]+)>");
                Regex pricePattern = new(@"<(Price:) (\d+,\d+|\d+)>");
                Regex genrePattern = new(@"<(Genre:) ([A-Za-z]+)>");
                Regex durationPattern = new(@"<(Duration:) ((([0-1]?[0-9])|([2][0-3]))(:([0-5][0-9])){1,2})>");
                Regex linkPattern = new(@"<(Link:) (http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?)>");
                Regex authorNamePattern = new(@"<(AuthorName:) ([A-Za-z_\s]+){1,3}>");

                var tmp = namePattern.Match(str);
                string name = namePattern.Match(str).Groups[2].Value;
                double price = double.Parse(pricePattern.Match(str).Groups[2].Value);
                string genre = genrePattern.Match(str).Groups[2].Value;
                TimeSpan duration = TimeSpan.Parse(durationPattern.Match(str).Groups[2].Value);
                string link = linkPattern.Match(str).Groups[2].Value;
                string authorName = authorNamePattern.Match(str).Groups[2].Value;                
                return new MovieProductModel(name, price, genre, duration, link, authorName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
