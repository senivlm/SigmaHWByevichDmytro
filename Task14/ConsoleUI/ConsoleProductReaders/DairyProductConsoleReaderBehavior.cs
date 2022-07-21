using Task14.Product;

namespace Task14.ConsoleUI.ConsoleProductReaders
{
    internal class DairyProductConsoleReaderBehavior : FoodProductConsoleReaderBehaviorBase, IConsoleProductReader<IDairyProduct>
    {
        public override IDairyProduct ConsoleReadProduct()
        {
            try
            {
                IDairyProduct _model = new DairyProductModel();
                ConsoleReadFoodProductBase(ref _model);
                return _model;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
