using Task14.Enums;
using Task14.Product;

namespace Task14.ConsoleUI.ConsoleProductReaders
{
    internal class MeatProductConsoleReaderBehavior : FoodProductConsoleReaderBehaviorBase, IConsoleProductReader<IMeatProduct>
    {
        public override IMeatProduct ConsoleReadProduct()
        {
            try
            {
                IMeatProduct _model = new MeatProductModel();
                ConsoleReadFoodProductBase(ref _model);
                ConsoleReadMeatSpecies(ref _model);
                ConsoleReadMeatCategory(ref _model);
                return _model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ConsoleReadMeatSpecies<T>(ref T product)
          where T : class, IMeatProduct
        {
            Console.Write("Введіть тип м'яса: ");
            string meatSpeciesLine = Console.ReadLine();
            if (Enum.TryParse(meatSpeciesLine, out MeatSpecies meatSpeciesResult) == false)
            {
                throw new ArgumentException("Хибний формат типу м'яса");
            }
            product.MeatSpeciesProp = meatSpeciesResult;
        }
        private void ConsoleReadMeatCategory<T>(ref T product)
          where T : class, IMeatProduct
        {
            Console.Write("Введіть категорію м'яса: ");
            string meatCategoryLine = Console.ReadLine();
            if (Enum.TryParse(meatCategoryLine, out MeatCategory meatCategoryResult) == false)
            {
                throw new ArgumentException("Хибний формат категорії м'яса");
            }
            product.MeatCategoryProp = meatCategoryResult;
        }
    }
}
