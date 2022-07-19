using Task11.Product;

namespace Task11.ConsoleUI.ConsoleProductReaders
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
