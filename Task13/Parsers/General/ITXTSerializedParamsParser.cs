using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task13.FileHandler
{
    internal delegate void LoggerOnBadFormat(string message);
    internal interface ITXTSerializedParamsParser<T>
    {
        T Parse(TXTSerializedParameters parameters);
        event LoggerOnBadFormat OnBadFormatLogger;
    }
}
