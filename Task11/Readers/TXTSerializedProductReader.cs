using System;
using System.IO;
using Task11.Validators;

namespace Task11.Readers
{
    internal class TXTSerializedProductReader<T> : IStreamLineReader<T>
        where T : IProduct
    {
        void IStreamLineReader<T>.ReadLine<G>(out G obj, StreamReader stream, IStringParser<G> validator)
        {
            obj = new();
            try
            {
                string line = stream.ReadLine();
                if (string.IsNullOrEmpty(line) == false)
                {
                    obj = validator.Parse(line);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


