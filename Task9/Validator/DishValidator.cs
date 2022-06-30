using System;

namespace Task9.Validator
{
    internal class DishValidator : IStringValidator<DishModel>
    {
        public bool IsValid(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            if (str.Trim().Split().Length != 1 && str.Trim().Split().Length != 2)
            {
                return false;
            }
            return true;
        }

        public void Validate(string str, DishModel obj)
        {
            string[] splitedStr = str.Trim().Split(" -".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (splitedStr.Length == 1)
            {
                if (!string.IsNullOrEmpty(obj.Name))
                {
                    throw new ArgumentException("Назва страви вже задана");
                }
                obj.Name = splitedStr[0];
            }
            else if (splitedStr.Length == 2)
            {
                if (string.IsNullOrEmpty(obj.Name))
                {
                    throw new ArgumentException("Хибний формат запи, можливо назва іде післе інгрідієнту");
                }
                if (!double.TryParse(splitedStr[1], out double weight))
                {
                    throw new ArgumentException("Хибний формат ваги");
                }
                if (!obj.TryAddIngridient(splitedStr[0], weight))
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new ArgumentException("Хибний формат запису");
            }
        }
    }
}
