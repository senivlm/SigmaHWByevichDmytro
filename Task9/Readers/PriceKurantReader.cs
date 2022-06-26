using System;
using System.IO;
using Task9.Validator;

namespace Task9.Readers
{
    internal class PriceKurantReader : IStreamReader<PriceKurantModel>
    {
        public void Read(out PriceKurantModel obj, StreamReader stream, IStringValidator<PriceKurantModel>? validator)
        {
            obj = new PriceKurantModel();
            try
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    if (validator?.IsValid(line) ?? true)
                    {
                        validator.Validate(line, obj);
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
