using System.Collections.Generic;
using System.IO;
using Task11.Validators;

namespace Task11.Readers
{
    internal interface IStreamCollectionReader<T, G>
        where T : IEnumerable<G>
    {
        void ReadCollection(out T obj, StreamReader stream, Dictionary<string, IStringParser<G>> validator);
        event LoggerOnBadFormat OnBadFormatLogger;

    }
}
