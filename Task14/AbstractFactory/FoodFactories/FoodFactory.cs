using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.Product;

namespace Task14.AbstractFactory.FoodFactories
{
    internal class FoodFactory : IFoodAbstractFactory
    {
        public IDairyProduct CreateDairyProduct() => new DairyProductModel();

        public IMeatProduct CreateMeatProduct() => new MeatProductModel();
    }
}
