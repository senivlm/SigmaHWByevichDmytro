using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsProj
{
    internal class Storage
    {
        private List<Product> _products;
        public int Count { get => _products.Count;  }
        public Storage(StorageBuilder storageBuilder)
        {
            _products = new List<Product>(storageBuilder.GetProducts());
        }
        public Storage(List<Product> products)
        {
            _products = new List<Product>( products);
        }

        public Product this[int index]
        {
            get { return _products[index]; }
            set { _products[index] = value; }
        }

    }
}
