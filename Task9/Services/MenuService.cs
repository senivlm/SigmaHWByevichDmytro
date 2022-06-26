using System.Collections.Generic;
using Task9.Exceptions;

namespace Task9
{
    internal delegate bool productNotFindExceptionBehevior(ref PriceKurantModel priceKurant, string name);
    internal static class MenuService
    {
        static public bool TryGetMenuTotalSum(MenuModel menu, PriceKurantModel priceKurant, out double menuTotalSum, productNotFindExceptionBehevior behevior)
        {
            menuTotalSum = default;
            foreach (DishModel dish in menu)
            {
                if (!TryGetDishPrice(dish, priceKurant, out double sumPrice, behevior))
                {
                    return false;
                }
                menuTotalSum += sumPrice;
            }
            return true;
        }
        static public Dictionary<string, double> GetIngridientsWeight(MenuModel menu)
        {
            Dictionary<string, double> ingrWeight = new Dictionary<string, double>();
            foreach (DishModel dish in menu)
            {
                foreach (KeyValuePair<string, double> ingridient in dish)
                {
                    if (ingrWeight.ContainsKey(ingridient.Key) == false)
                    {
                        ingrWeight.Add(ingridient.Key, ingridient.Value);
                    }
                    else
                    {
                        ingrWeight[ingridient.Key] += ingridient.Value;
                    }

                }
            }
            return ingrWeight;
        }
        static public bool TryGetDishPrice(DishModel dish, PriceKurantModel priceKurant, out double sumPrice, productNotFindExceptionBehevior behevior)
        {
            sumPrice = default;
            foreach (string key in dish.Keys)
            {
                if (priceKurant.ContainsKey(key) == false)
                {
                    if (behevior is not null)
                    {
                        try
                        {
                            while (behevior(ref priceKurant, key) == false)
                            {
                                ;
                            }
                        }
                        catch (ProductNotFoundException)
                        {
                            return false;
                        }
                    }
                }
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
