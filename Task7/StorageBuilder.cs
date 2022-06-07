using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
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
        public StorageBuilder AddProductList(IEnumerable<Product> products)
        {
            this._products.AddRange(products.ToList());
            return this;
        }
        public StorageBuilder ConsoleMenuInput()
        {
            var options = new List<Option>()
            {
               new Option("Add meat product", () => ConsoleAddMeatProduct()),
               new Option("Add diary product", () => ConsoleAddDairyProduct())
            };
            var menu = new Menu(options);
            menu.PrintMenu();
            return this;
        }
        public Storage Build()
        {
            return new Storage(this);
        }
        private void ConsoleAddMeatProduct()
        {
            var meat = new Meat();
            meat.ConsoleSet();
            _products.Add(meat);
        }
        private void ConsoleAddDairyProduct()
        {
            var dairy = new DairyProduct();
            dairy.ConsoleSet();
            _products.Add(dairy);
        }
    }
}
