using System;
using Task9.Models;

namespace Task9.Validator
{
    internal class ChangerValidator : IStringValidator<ChangerModel>
    {
        public bool IsValid(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return (str.Trim().Split("- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length == 2);
        }

        public void Validate(string str, ChangerModel obj)
        {
            if (!IsValid(str))
            {
                throw new ArgumentException("Хибний формат запису");
            }
            var splitedStr = str.Trim().Split("- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (!double.TryParse(splitedStr[1], out double price))
            {
                throw new ArgumentException("Хибний формат ціни");
            }
            try
            {
                obj.Add(splitedStr[0], price);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
