using System;
using Task11.Validators;

namespace Task11.Parsers
{
    internal abstract class ProductParserBase : IStringParser<IProduct>
    {
        public abstract IProduct Parse(string str);
    }
}
