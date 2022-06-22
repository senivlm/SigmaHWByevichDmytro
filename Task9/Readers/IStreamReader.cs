using System.IO;
using Task9.Validator;

namespace Task9.Readers
{
    internal interface IStreamReader<T>
    {
        void Read(out T obj, StreamReader stream, IStringValidator<T> validator);
    }
}
