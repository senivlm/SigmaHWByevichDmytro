using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Task11.Validators;

namespace Task11.Readers
{
    internal class TXTSerializedStorageReader<T> : IStreamCollectionReader<ProductStorage<T>, T>
        where T : IProduct
    {
        public void ReadCollection(out ProductStorage<T> obj, StreamReader stream, Dictionary<string, IStringParser<T>> validator)
        {
            obj = new();
            try
            {
                while (stream.EndOfStream == false)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        Regex typePattern = new("^<([A-Za-z]+)>");
                        string type = typePattern.Match(line).Groups[1].Value;
                        if (string.IsNullOrEmpty(type) == false)
                        {
                            if (validator.ContainsKey(type))
                            {
                                IStringParser<T> Linevalidator = validator[type];
                                obj.Add(Linevalidator.Parse(line));
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
