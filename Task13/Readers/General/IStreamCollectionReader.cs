using System.Collections.Generic;
using System.IO;
using Task13.FileHandler;

namespace Task13.Readers
{
    internal interface IStreamCollectionReader<T, G>
        where T : IEnumerable<G>
    {
        void ReadCollection(ref T obj, StreamReader stream, Dictionary<string, ITXTSerializedParamsParser<G>> validator);
        event LoggerOnBadFormat OnBadFormatLogger;

    }
}
