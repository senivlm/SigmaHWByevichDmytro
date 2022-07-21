using System.Text;
using System.Text.RegularExpressions;
using Task14.FileHandler;
using Task14.Validators;

namespace Task14.Parsers
{
    internal abstract class FoodProductParserBase : ProductParserBase, ITXTSerializedParametersParser<IFoodProduct>
    {
        protected Regex _changePriceByDaysElementPattern = new(@"{(\[[\d]+[,][\s][\d]+\])*}");

        public abstract override event LoggerOnBadFormat OnBadFormatLogger;
        public abstract override IFoodProduct Parse(TXTSerializedParameters txtSerializedParams);
        protected FoodProductParserBase() : base()
        { }
        protected FoodProductParserBase(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }
        protected virtual void FoodProductValidate<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IFoodProduct
        {
            ProductValidate(ref product, in txtSerializedParams, in logDescriptionLine);
            ValidateWeight(ref product, in txtSerializedParams, in logDescriptionLine);
            ValidateExpirationTime(ref product, in txtSerializedParams, in logDescriptionLine);
            ValidateChangePriceByDaysDictionary(ref product, in txtSerializedParams, in logDescriptionLine);
        }
        protected virtual void ValidateWeight<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IFoodProduct
        {
            if (txtSerializedParams.ContainsKey("Weight") == false)
            {
                logDescriptionLine.Append("Не знайдено вагу продукту; ");
            }
            else
            {
                if (double.TryParse(txtSerializedParams["Weight"], out double resultWeight) == false)
                {
                    logDescriptionLine.Append("Хибний формат ваги; ");
                }
                else
                {
                    product.Weight = resultWeight;
                }
            }
        }
        protected virtual void ValidateExpirationTime<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IFoodProduct
        {
            if (txtSerializedParams.ContainsKey("ExpirationTime") == false)
            {
                logDescriptionLine.Append("Не знайдено термін придатності продукту; ");
            }
            else
            {
                if (DateTime.TryParse(txtSerializedParams["ExpirationTime"], out DateTime resultexpirationTime) == false)
                {
                    logDescriptionLine.Append("Хибний терміну придатності; ");
                }
                else
                {
                    product.ExpirationTime = resultexpirationTime;
                }
            }
        }
        protected virtual void ValidateChangePriceByDaysDictionary<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IFoodProduct
        {
            if (txtSerializedParams.ContainsKey("DaysToExpirationAndPresentOfChange") == false)
            {
                logDescriptionLine.Append("Не знайдено список змін ціни за терміном придатності; ");
            }
            else
            {
                Match changePriceByDaysElementMatch = _changePriceByDaysElementPattern.Match(txtSerializedParams["DaysToExpirationAndPresentOfChange"]);
                if (changePriceByDaysElementMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено значень у списку змін ціни за терміном придатності; ");
                }
                else
                {
                    SortedDictionary<int, int> changePriceByDaysDictionary = new();
                    foreach (Capture item in changePriceByDaysElementMatch.Groups[1].Captures)
                    {
                        string[] values = item.ToString().Trim("[]".ToCharArray()).Split(", ");
                        if (int.TryParse(values[0], out int key) && int.TryParse(values[1], out int value))
                        {
                            if (changePriceByDaysDictionary.TryAdd(key, value) == false)
                            {
                                logDescriptionLine.Append("Не вдалося додати значення до списку змін ціни за терміном придатності, можливо воно повторюється; ");
                                break;
                            }
                        }
                        else
                        {
                            logDescriptionLine.Append("Хибний формат значень у списку змін ціни за терміном придатності; ");
                        }
                    }
                    product.DaysToExpirationAndPresentOfChange = changePriceByDaysDictionary;
                }
            }
        }
    }
}
