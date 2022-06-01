using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Storage
    {
        private List<Product> _products;
        public int Count { get => _products.Count;  }
        public Storage()
        {
            _products = new List<Product>();
        }
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
        public void ChangePrice(int precent)
        {
            foreach (Product product in _products)  
                product.ChangePrice(precent);
        }
        public void PrintProducts()
        {
            Console.WriteLine("\nProducts storage:");
            foreach (Product product in _products)
                Console.WriteLine(product);
        }
        public List<Product> GetMeatProducts()
        {
            List<Product> meatProducts = new List<Product>();
            meatProducts = _products.Where(x => x is Meat).ToList();
            return meatProducts;
        }
    }
}
