using Task11.Validators;

namespace Task11.Parsers
{
    internal abstract class ProductParserBase : IStringParser<IProduct>
    {
        public abstract event LoggerOnBadFormat OnBadFormatLogger;
        public abstract IProduct Parse(string str);
        protected ProductParserBase()
        {

        }
        protected ProductParserBase(LoggerOnBadFormat loggerOnBad)
        {
            OnBadFormatLogger += loggerOnBad;
        }
    }
}
