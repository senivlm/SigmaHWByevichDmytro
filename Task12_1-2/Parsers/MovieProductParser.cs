using System;
using System.Text;
using System.Text.RegularExpressions;
using Task11.Product.General;
using Task11.Product.MovieProduct;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MovieProductParser : ProductParserBase, IStringParser<IMovieProduct>
    {
        private Regex _namePattern = new(@"<(Name:) ([^>]*)>");
        private Regex _pricePattern = new(@"<(Price:) ([^>]*)>");
        private Regex _genrePattern = new(@"<(Genre:) ([^>]*)>");
        private Regex _durationPattern = new(@"<(Duration:) ([^>]*)>");
        private Regex _linkPattern = new(@"<(Link:) ([^>]*)>");
        private Regex _authorNamePattern = new(@"<(AuthorName:) ([^>]*)>");
        public MovieProductParser()
        { }

        public MovieProductParser(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }

        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override IMovieProduct Parse(string str)
        {
            try
            {
                IMovieProduct _model = new MovieProductModel();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);

                #region Matches
                Match nameMatch = _namePattern.Match(str);
                Match priceMatch = _pricePattern.Match(str);
                Match genreMatch = _genrePattern.Match(str);
                Match durationMatch = _durationPattern.Match(str);
                Match linkMatch = _linkPattern.Match(str);
                Match authorNameMatch = _authorNamePattern.Match(str);
                #endregion

                #region NameValidate
                if (nameMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено назву продукта; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(nameMatch.Groups[2].Value))
                    {
                        logDescriptionLine.Append("Хибний формат назви; ");
                    }
                    else
                    {
                        _model.Name = nameMatch.Groups[2].Value;
                    }
                }
                #endregion
                #region PriceValidate
                if (priceMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено ціну продукту; ");
                }
                else
                {
                    if (double.TryParse(priceMatch.Groups[2].Value, out double resultPrice) == false)
                    {
                        logDescriptionLine.Append("Хибний формат ціни; ");
                    }
                    else
                    {
                        _model.Price = resultPrice;
                    }
                }
                #endregion
                #region GenreValidate
                if (genreMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено жанр продукту; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(genreMatch.Groups[2].Value))
                    {
                        logDescriptionLine.Append("Хибний формат жанру; ");
                    }
                    else
                    {
                        _model.Genre = genreMatch.Groups[2].Value;
                    }
                }
                #endregion
                #region DurationValidate
                if (durationMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено тривалість продукту; ");
                }
                else
                {
                    if (TimeSpan.TryParse(durationMatch.Groups[2].Value, out TimeSpan resultDuration) == false)
                    {
                        logDescriptionLine.Append("Хибний формат тривалості; ");
                    }
                    else
                    {
                        _model.Duration = resultDuration;
                    }
                }
                #endregion
                #region LinkValidate
                if (linkMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено посилання продукту; ");
                }
                else
                {
                    if (Uri.IsWellFormedUriString(linkMatch.Groups[2].Value, UriKind.Absolute) == false)
                    {
                        logDescriptionLine.Append("Хибний формат посилання; ");
                    }
                    else
                    {
                        _model.Link = linkMatch.Groups[2].Value;
                    }
                }
                #endregion
                #region AuthorNameValidate
                if (authorNameMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено ім'я автора продукту; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(authorNameMatch.Groups[2].Value))
                    {
                        logDescriptionLine.Append("Хибний формат імені автора; ");
                    }
                    else
                    {
                        _model.AuthorName = authorNameMatch.Groups[2].Value;
                    }
                }
                #endregion

                if (logDescriptionLine.Length != discriptionConst.Length)
                {
                    logDescriptionLine.Append(" >;");
                    OnBadFormatLogger?.Invoke(str + logDescriptionLine.ToString());
                    return null;
                }

                return _model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
