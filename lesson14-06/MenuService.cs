namespace lesson14_06
{
    internal static class MenuService
    {
        public static bool TryGetMenuTotalSum(Menu menu, PriceKurant priceKurant, out double menuTotalSum)
        {
            menuTotalSum = default;
            for (int i = 0; i < menu.Length; i++)
            {
                if (!TryGetDishPrice(menu[i], priceKurant, out double sumPrice))
                {
                    return false;
                }

                menuTotalSum += sumPrice;
            }
            return true;
        }
        public static bool TryGetDishPrice(Dish dish, PriceKurant priceKurant, out double sumPrice)
        {
            sumPrice = default;
            foreach (string key in dish.Keys)
            {
                if (!priceKurant.TryGetProductPrice(key, out double poductPrice))
                {
                    return false;
                }
                sumPrice += poductPrice * dish[key];
            }
            return true;

        }
    }
}
