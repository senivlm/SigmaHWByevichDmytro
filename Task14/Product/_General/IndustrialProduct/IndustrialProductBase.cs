using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task14.Product._General.IndustrialProduct
{
    internal abstract class IndustrialProductBase : ProductBase, IIndustrialProduct
    {
        protected double _weight;
        public virtual double Weight
        {
            get => _weight;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                _weight = value;
            }
        }
        protected IndustrialProductBase() : this(default, default, default) { }
        protected IndustrialProductBase(string name, double price, double weight) : base(name, price)
        {
            Weight = weight;
        }
        public override string ToString()
        {
            return base.ToString() + $"Вага: {Weight}; ";
        }
    }
}
