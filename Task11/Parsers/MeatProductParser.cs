using System;
using System.Text;
using Task11.Enums;
using Task11.FileHandler;
using Task11.Product;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MeatProductParser : FoodProductParserBase, ITXTSerializedParametersParser<IMeatProduct>
    {
        public MeatProductParser() : base()
        { }

        public MeatProductParser(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }

        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override IMeatProduct Parse(TXTSerializedParameters txtSerializedParams)
        {
            try
            {
                IMeatProduct _model = new MeatProductModel();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);
                FoodProductValidate(ref _model, in txtSerializedParams, in logDescriptionLine);
                ValidatMeatSpecies(ref _model, in txtSerializedParams, in logDescriptionLine);
                ValidatMeatCategory(ref _model, in txtSerializedParams, in logDescriptionLine);
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
        private void ValidatMeatSpecies<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
           where T : IMeatProduct
        {
            if (txtSerializedParams.ContainsKey("MeatSpeciesProp") == false)
            {
                logDescriptionLine.Append("Не знайдено тип м'яса; ");
            }
            else
            {
                if (Enum.TryParse(txtSerializedParams["MeatSpeciesProp"], out MeatSpecies resultMeatSpecies) == false)
                {
                    logDescriptionLine.Append("Хибний формат типу м'яса; ");
                }
                else
                {
                    product.MeatSpeciesProp = resultMeatSpecies;
                }
            }
        }
        private void ValidatMeatCategory<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
           where T : IMeatProduct
        {
            if (txtSerializedParams.ContainsKey("MeatCategoryProp") == false)
            {
                logDescriptionLine.Append("Не знайдено категорію м'яса; ");
            }
            else
            {
                if (Enum.TryParse(txtSerializedParams["MeatCategoryProp"], out MeatCategory resultMeatCategory) == false)
                {
                    logDescriptionLine.Append("Хибний формат категорії м'яса; ");
                }
                else
                {
                    product.MeatCategoryProp = resultMeatCategory;
                }
            }
        }
    }
}
