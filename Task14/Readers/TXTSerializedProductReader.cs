using Task14.FileHandler;
using Task14.Validators;

namespace Task14.Readers
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


