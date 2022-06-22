using System;
using System.IO;
using Task9.Validator;

namespace Task9.Readers
{
    internal class DishReader : IStreamReader<DishModel>
    {
        public void Read(out DishModel obj, StreamReader stream, IStringValidator<DishModel> validator)
        {
            obj = new DishModel();
            try
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    if (validator.IsValid(line))
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
