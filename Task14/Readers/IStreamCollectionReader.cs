using Task14.Validators;

namespace Task14.Readers
{
    internal interface IStreamCollectionReader<T, G>
        where T : IEnumerable<G>
    {
        void ReadCollection(ref T obj, StreamReader stream, Dictionary<string, ITXTSerializedParametersParser<G>> validator);
        event LoggerOnBadFormat OnBadFormatLogger;

    }
}
