using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task13.FileHandler;
using Task13.Persons;

namespace Task13.Readers
{
    internal class TXTSerializedPersonReader<T> : IObjectGetterFromSerializedLine<T>
        where T : IPerson
    {
        public bool TryGetObject<G>(out G obj, string line, ITXTSerializedParamsParser<G> parser) where G : T, new()
        {
            obj = new();
            try
            {
                if (string.IsNullOrEmpty(line) == false)
                {
                    obj = parser.Parse(new TXTSerializedLineAnalyzer().GetTXTSerializedParameters(line));
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
