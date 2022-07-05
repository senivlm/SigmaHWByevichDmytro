using System;
using System.IO;
using Task11.FileHandler;
using Task11.Parsers;
using Task11.Validators;

namespace Task11.Readers
{
    internal class TXTSerializedProductReader<T> : IStreamLineReader<T>
        where T : IProduct
    {
        bool IStreamLineReader<T>.TryReadLine<G>(out G obj, StreamReader stream, IStringParser<G> validator)
        {
            obj = new();
            try
            {
                string line = stream.ReadLine();
                if (string.IsNullOrEmpty(line) == false)
                {
                    TXTSerializedLineAnalyzer tXTSerializedLineAnalyzer = new();
                    TXTSerializedParameters LineParams = tXTSerializedLineAnalyzer.GetTXTSerializedParameters(stream.ReadLine());
                    obj = validator.Parse(LineParams);
                    return obj is not null;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


