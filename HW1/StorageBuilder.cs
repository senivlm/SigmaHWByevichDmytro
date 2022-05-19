using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsProj
{
    internal class StorageBuilder
    {
        private List<Product> _products;
        public List<Product> GetProducts()
        {
            return _products;
        }
        public StorageBuilder()
        {
            _products = new List<Product>();
        }
        public StorageBuilder AddProduct(Product product)
        {
            _products.Add(product);
            return this;
        }
        public StorageBuilder AddProductList(List<Product> products)
        {
            this._products.AddRange(products);
            return this;
        }
        //доробити меню
        public StorageBuilder ConsoleMenuInput()
        {
            return this;
        }

        public Storage Build()
        {
            return new Storage(this);
        }

        

    }
}
