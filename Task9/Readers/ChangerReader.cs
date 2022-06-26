using System;
using System.IO;
using Task9.Models;
using Task9.Validator;

namespace Task9.Readers
{
    internal class ChangerReader : IStreamReader<ChangerModel>
    {
        public void Read(out ChangerModel obj, StreamReader stream, IStringValidator<ChangerModel> validator)
        {
            obj = new ChangerModel();
            try
            {
                while (!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line) == false && validator.IsValid(line))
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
