using Task9.Models;

namespace Task9.Services
{
    internal static class ChangerService
    {
        public static bool TryConvert(string currency, double value, ChangerModel changer, out double result)
        {
            result = default;
            if (changer.ContainsCurrency(currency) && changer.TryGetValue(currency, out result))
            {
                result *= value;
                return true;
            }
            return false;
        }
    }
}
