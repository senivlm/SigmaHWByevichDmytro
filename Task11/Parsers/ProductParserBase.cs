using System;
using Task11.Validators;

namespace Task11.Parsers
{
    internal abstract class ProductParserBase : IStringParser<IProduct>
    {
        public virtual IProduct Parse(string str)
        {
            throw new NotImplementedException();
        }
    }
}
