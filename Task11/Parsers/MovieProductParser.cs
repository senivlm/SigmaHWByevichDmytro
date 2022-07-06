using System;
using System.Text;
using Task11.FileHandler;
using Task11.Product.General;
using Task11.Product.MovieProduct;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MovieProductParser : ProductParserBase, ITXTSerializedParametersParser<IMovieProduct>
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
                ValidateGenre(ref _model, in txtSerializedParams, in logDescriptionLine);
                ValidateDuration(ref _model, in txtSerializedParams, in logDescriptionLine);
                ValidateLink(ref _model, in txtSerializedParams, in logDescriptionLine);
                ValidateAuthorName(ref _model, in txtSerializedParams, in logDescriptionLine);

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
        private void ValidateGenre<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
           where T : IMovieProduct
        {
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
                    product.Genre = txtSerializedParams["Genre"];
                }
            }
        }
        private void ValidateDuration<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
           where T : IMovieProduct
        {
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
                    product.Duration = resultDuration;
                }
            }
        }
        private void ValidateLink<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
          where T : IMovieProduct
        {
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
                    product.Link = txtSerializedParams["Link"];
                }
            }
        }
        private void ValidateAuthorName<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
          where T : IMovieProduct
        {
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
                    product.AuthorName = txtSerializedParams["AuthorName"];
                }
            }
        }


    }
}
