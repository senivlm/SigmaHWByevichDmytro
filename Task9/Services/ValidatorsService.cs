using Task9.Models;
using Task9.Validator;

namespace Task9.Services
{
    internal static class ValidatorsService
    {
        public static IStringValidator<DishModel> DishValidator => new DishValidator();
        public static IStringValidator<PriceKurantModel> PriceKurantValidator => new IngridientValidator();
        public static IStringValidator<ChangerModel> ChangerValidator => new ChangerValidator();
    }
}
