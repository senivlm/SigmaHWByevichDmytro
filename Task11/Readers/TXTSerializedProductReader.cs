using System;
using Task11.FileHandler;
using Task11.Validators;

namespace Task11.Readers
{
    internal class TXTSerializedProductReader<T> : IObjectGetterFromSerializedLine<T>
        where T : IProduct
    {
        bool IObjectGetterFromSerializedLine<T>.TryGetObject<G>(out G obj, string line, ITXTSerializedParametersParser<G> validator)
        {
            obj = new();
            try
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    TXTSerializedLineAnalyzer tXTSerializedLineAnalyzer = new();
                    TXTSerializedParameters LineParams = tXTSerializedLineAnalyzer.GetTXTSerializedParameters(line);
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


