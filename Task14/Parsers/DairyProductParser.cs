using System.Text;
using Task14.FileHandler;
using Task14.Parsers;
using Task14.Product;

namespace Task14.Validators
{
    internal class DairyProductParser : FoodProductParserBase, ITXTSerializedParametersParser<IDairyProduct>
    {
        public DairyProductParser() : base()
        { }

        public DairyProductParser(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }

        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override IDairyProduct Parse(TXTSerializedParameters txtSerializedParams)
        {
            try
            {
                IDairyProduct _model = new DairyProductModel();
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
