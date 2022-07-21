using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.FileHandler;
using Task14.Product._General.IndustrialProduct;
using Task14.Validators;

namespace Task14.Parsers.General
{
    internal abstract class IndustrialProductParserBase : ProductParserBase, ITXTSerializedParametersParser<IIndustrialProduct>
    {
        public abstract override event LoggerOnBadFormat OnBadFormatLogger;
        public abstract override IIndustrialProduct Parse(TXTSerializedParameters txtSerializedParams);


        protected virtual void IndustrialProductValidate<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
            where T : IIndustrialProduct
        {
            ProductValidate(ref product, in txtSerializedParams, in logDescriptionLine);
            ValidateWeight(ref product, in txtSerializedParams, in logDescriptionLine);
            
        }
        protected virtual void ValidateWeight<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
           where T : IIndustrialProduct
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

    }
}
