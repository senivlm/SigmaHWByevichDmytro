using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task14.Product._General.IndustrialProduct;

namespace Task14.AbstractFactory.IndustrailFactories
{
    internal interface IIndustrialAbstractFactory
    {
        IIndustrialProduct CreateCementProduct();
    }
}
