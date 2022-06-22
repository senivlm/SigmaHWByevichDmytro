using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Validator;

namespace Task9.Services
{
    internal static class ValidatorsService
    {
        public static IStringValidator<DishModel> DishValidator => new DishValidator(); 
    }
}
