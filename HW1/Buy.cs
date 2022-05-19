using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsProj
{
    internal class Buy
    {
        private List<Product> _products;

        public List<Product> ProductList
        {
            get { return new List<Product>(_products); }
        }
        private double _productWeightSum;

        public  double ProductWeightSum
        {
            get { return _productWeightSum; }
            private set { _productWeightSum = value; }
        }

        private double _productPriceSum;

        public double ProductPriceSum
        {
            get { return _productPriceSum; }
            private set { _productPriceSum = value; }
        }


        public Buy() : this(new List<Product>()) { }
        public Buy(IEnumerable<Product> products)
        {
            _products = new List<Product>(products);
            ProductWeightSum = _products.Select(x=>x.Weight).Sum();
            ProductPriceSum = _products.Select(x=>x.Price).Sum();
        }
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException(nameof(obj));
            if (obj is Buy other &&
                other.ProductList.Count == this.ProductList.Count)
            {
                for (int i = 0; i < ProductList.Count; i++)
                {
                    if (!Equals(ProductList[i], other.ProductList[i]))
                        return false;
                }
                return true;
            }
            return false;

            
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(Product product in _products)
                builder.Append(product.ToString());
            builder.Append($"Total weight: {ProductWeightSum}; ");
            builder.Append($"Total price: {ProductPriceSum}; ");
            return builder.ToString();
        }
    }
}
