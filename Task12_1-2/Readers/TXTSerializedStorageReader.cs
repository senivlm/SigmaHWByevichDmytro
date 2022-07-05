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
        public event LoggerOnBadFormat OnBadFormatLogger;
        public TXTSerializedStorageReader()
        {

        }
        public TXTSerializedStorageReader(LoggerOnBadFormat onBadFormatLogger)
        {
            OnBadFormatLogger += onBadFormatLogger;
        }

        public void ReadCollection(ref ProductStorage<T> obj, StreamReader stream, Dictionary<string, IStringParser<T>> validator)
        {
            if (obj is null)
            {
                obj = new();
            }
            try
            {
                while (stream.EndOfStream == false)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        Regex typePattern = new("^<([A-Za-z]+)>");
                        Match typeMatch = typePattern.Match(line);
                        if (typeMatch.Success)
                        {
                            string type = typePattern.Match(line).Groups[1].Value;
                            if (validator.ContainsKey(type))
                            {
                                T tmpRes = validator[type].Parse(line);
                                if (tmpRes is not null)
                                {
                                    obj.Add(tmpRes);
                                }
                            }
                            else
                            {
                                OnBadFormatLogger?.Invoke(line + "< Description: " + "Набір парсерів не має парсера для цього >;");
                            }
                        }
                        else
                        {
                            OnBadFormatLogger?.Invoke(line + "< Description: " + "Хибний формат запису типу даних>;");
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
