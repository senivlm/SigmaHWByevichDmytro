using System;

namespace Task9.Validator
{
    internal class IngridientValidator : IStringValidator<PriceKurantModel>
    {
        public bool IsValid(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return (str.Trim().Split("- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length == 2);

        }

        public void Validate(string str, PriceKurantModel obj)
        {

            if (!IsValid(str))
            {
                throw new ArgumentException("Хибний формат запису");
            }
            string[] splitedStr = str.Trim().Split("- ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
