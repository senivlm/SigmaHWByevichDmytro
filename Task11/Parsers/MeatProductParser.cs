using System;
using System.Text;
using Task11.FileHandler;
using Task11.Product;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MeatProductParser : FoodProductParserBase
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
