using System;
using System.Text;
using Task11.FileHandler;
using Task11.Product.General;
using Task11.Product.MovieProduct;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MovieProductParser : ProductParserBase, IStringParser<IMovieProduct>
    {

        public MovieProductParser()
        { }

        public MovieProductParser(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }

        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override IMovieProduct Parse(TXTSerializedParameters txtSerializedParams)
        {
            try
            {
                IMovieProduct _model = new MovieProductModel();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);


                ProductValidate(ref _model, in txtSerializedParams, in logDescriptionLine);
                #region GenreValidate
                if (txtSerializedParams.ContainsKey("Genre") == false)
                {
                    logDescriptionLine.Append("Не знайдено жанр продукту; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(txtSerializedParams["Genre"]))
                    {
                        logDescriptionLine.Append("Хибний формат жанру; ");
                    }
                    else
                    {
                        _model.Genre = txtSerializedParams["Genre"];
                    }
                }
                #endregion
                #region DurationValidate
                if (txtSerializedParams.ContainsKey("Duration") == false)
                {
                    logDescriptionLine.Append("Не знайдено тривалість продукту; ");
                }
                else
                {
                    if (TimeSpan.TryParse(txtSerializedParams["Duration"], out TimeSpan resultDuration) == false)
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
                if (txtSerializedParams.ContainsKey("Link") == false)
                {
                    logDescriptionLine.Append("Не знайдено посилання продукту; ");
                }
                else
                {
                    if (Uri.IsWellFormedUriString(txtSerializedParams["Link"], UriKind.Absolute) == false)
                    {
                        logDescriptionLine.Append("Хибний формат посилання; ");
                    }
                    else
                    {
                        _model.Link = txtSerializedParams["Link"];
                    }
                }
                #endregion
                #region AuthorNameValidate
                if (txtSerializedParams.ContainsKey("AuthorName") == false)
                {
                    logDescriptionLine.Append("Не знайдено ім'я автора продукту; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(txtSerializedParams["AuthorName"]))
                    {
                        logDescriptionLine.Append("Хибний формат імені автора; ");
                    }
                    else
                    {
                        _model.AuthorName = txtSerializedParams["AuthorName"];
                    }
                }
                #endregion

                if (logDescriptionLine.Length != discriptionConst.Length)
                {
                    logDescriptionLine.Append(" >;");
                    OnBadFormatLogger?.Invoke(txtSerializedParams.PrimalLine + logDescriptionLine.ToString());
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
