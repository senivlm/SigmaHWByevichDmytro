using System.Collections.Generic;
using System.IO;
using Task11.Validators;

namespace Task11.Readers
{
    internal interface IStreamCollectionReader<T, G>
        where T : IEnumerable<G>
    {
        void ReadCollection(ref T obj, StreamReader stream, Dictionary<string, ITXTSerializedParametersParser<G>> validator);
        event LoggerOnBadFormat OnBadFormatLogger;

    }
}
