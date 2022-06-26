using System;
using System.IO;
using Task9.Services;
using Task9.Validator;

namespace Task9.Readers
{
    internal class MenuReader : IStreamReader<MenuModel>
    {
        public void Read(out MenuModel obj, StreamReader stream, IStringValidator<MenuModel>? validator)
        {
            obj = new MenuModel();
            DishReader dishReader = new DishReader();
            try
            {
                while (!stream.EndOfStream)
                {
                    dishReader.Read(out DishModel dish, stream, ValidatorsService.DishValidator);
                    if (string.IsNullOrEmpty(dish.Name) == false)
                    {
                        obj.Add(dish);
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
