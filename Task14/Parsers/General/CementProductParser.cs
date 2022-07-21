using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.Enums;
using Task14.FileHandler;
using Task14.Product._General.IndustrialProduct;
using Task14.Product.CementProduct;
using Task14.Validators;

namespace Task14.Parsers.General
{
    internal class CementProductParser : IndustrialProductParserBase, ITXTSerializedParametersParser<ICementProduct>
    {
        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override ICementProduct Parse(TXTSerializedParameters txtSerializedParams)
        {
            try
            {
                ICementProduct _model = new CementProductModel();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);
                IndustrialProductValidate(ref _model, in txtSerializedParams, in logDescriptionLine);
                CementBrandValidate(ref _model, in txtSerializedParams, in logDescriptionLine);

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
        private void CementBrandValidate<T>(ref T product, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
          where T : ICementProduct
        {
            if (txtSerializedParams.ContainsKey("CementBrand") == false)
            {
                logDescriptionLine.Append("Не знайдено марку цементу; ");
            }
            else
            {
                if (Enum.TryParse(txtSerializedParams["CementBrand"], out CementBrand resultCementBrand) == false)
                {
                    logDescriptionLine.Append("Хибний формат марки цемента; ");
                }
                else
                {
                    product.CementBrand = resultCementBrand;
                }
            }
        }
    }
}
