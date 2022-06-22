using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Services;
using Task9.Validator;

namespace Task9.Readers
{
    internal class MenuReader : IStreamReader<MenuModel>
    {
        public void Read(out MenuModel obj, StreamReader stream, IStringValidator<MenuModel> validator)
        {
            obj = new MenuModel();
            DishReader dishReader = new DishReader();
            try
            {
                while (!stream.EndOfStream)
                {
                    dishReader.Read(out DishModel dish1, stream, ValidatorsService.DishValidator);
                    if (!string.IsNullOrEmpty(dish1.Name))
                    {
                        obj.Add(dish1);
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
