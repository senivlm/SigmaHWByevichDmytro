using System.IO;
using Task11.Validators;

namespace Task11.Readers
{
    internal interface IStreamLineReader<T>
    {
        void ReadLine<G>(out G obj, StreamReader stream, IStringParser<G>? validator) where G : T, new();
    }
}
