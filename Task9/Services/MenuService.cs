namespace Task9
{
    internal static class MenuService
    {
        static public bool TryGetMenuTotalSum(MenuModel menu, PriceKurantModel priceKurant, out double menuTotalSum)
        {
            menuTotalSum = default;
            foreach (DishModel dish in menu)
            {
                if (!TryGetDishPrice(dish, priceKurant, out double sumPrice))
                {
                    return false;
                }
                menuTotalSum += sumPrice;
            }
            return true;
        }
        static public bool TryGetDishPrice(DishModel dish, PriceKurantModel priceKurant, out double sumPrice)
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
