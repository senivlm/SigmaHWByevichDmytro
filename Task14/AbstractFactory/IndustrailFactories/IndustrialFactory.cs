using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.Product._General.IndustrialProduct;
using Task14.Product.CementProduct;

namespace Task14.AbstractFactory.IndustrailFactories
{
    internal class IndustrialFactory : IIndustrialAbstractFactory
    {
        public IIndustrialProduct CreateCementProduct() => new CementProductModel();
    }
}
