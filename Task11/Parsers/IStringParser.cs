using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Validators
{
    internal interface IStringParser<T>
    {
        T Parse(string str);
    }
}
