using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.Product;

namespace Task14.AbstractFactory.FoodFactories
{
    internal interface IFoodAbstractFactory
    {
        IMeatProduct CreateMeatProduct();
        IDairyProduct CreateDairyProduct();
    }
}
