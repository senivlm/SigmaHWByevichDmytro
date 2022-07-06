using System.Text;
using Task11.FileHandler;
using Task11.Validators;

namespace Task11.Parsers
{
    internal abstract class ProductParserBase : IStringParser<IProduct>
    {
        public abstract event LoggerOnBadFormat OnBadFormatLogger;
        public abstract IProduct Parse(TXTSerializedParameters txtSerializedParams);
        protected ProductParserBase()
        { }
        protected ProductParserBase(LoggerOnBadFormat loggerOnBad)
        {
            OnBadFormatLogger += loggerOnBad;
        }
        protected virtual void ProductValidate<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IProduct
        {
            ValidateName(ref product, in txtSerializedParams, in logDescriptionLine);
            ValidatePrice(ref product, in txtSerializedParams, in logDescriptionLine);
        }

        protected virtual void ValidateName<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IProduct
        {
            if (txtSerializedParams.ContainsKey("Name") == false)
            {
                logDescriptionLine.Append("Не знайдено назву продукта; ");
            }
            else
            {
                if (string.IsNullOrEmpty(txtSerializedParams["Name"]))
                {
                    logDescriptionLine.Append("Хибний формат назви; ");
                }
                else
                {
                    product.Name = txtSerializedParams["Name"];
                }
            }
        }
        protected virtual void ValidatePrice<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IProduct
        {
            if (txtSerializedParams.ContainsKey("Price") == false)
            {
                logDescriptionLine.Append("Не знайдено ціну продукту; ");
            }
            else
            {
                if (double.TryParse(txtSerializedParams["Price"], out double resultPrice) == false)
                {
                    logDescriptionLine.Append("Хибний формат ціни; ");
                }
                else
                {
                    product.Price = resultPrice;
                }
            }
        }
    }
}
