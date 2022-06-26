using Task9.Models;
using Task9.Readers;

namespace Task9.Services
{
    internal static class StreamReadersService
    {
        public static IStreamReader<DishModel> DishReader => new DishReader();
        public static IStreamReader<MenuModel> MenuReader => new MenuReader();
        public static IStreamReader<PriceKurantModel> PriceKurantReader => new PriceKurantReader();
        public static IStreamReader<ChangerModel> ChangerReader => new ChangerReader();


    }
}
