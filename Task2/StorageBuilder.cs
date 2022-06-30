using System.Collections.Generic;

namespace Task2
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
            List<Option> options = new List<Option>()
            {
               new Option("Add meat product", () => ConsoleAddMeatProduct()),
               new Option("Add diary product", () => ConsoleAddDairyProduct())
            };
            Menu menu = new Menu(options);
            menu.PrintMenu();
            return this;
        }

        public Storage Build()
        {
            return new Storage(this);
        }
        private void ConsoleAddMeatProduct()
        {
            Meat meat = new Meat();
            meat.ConsoleSet();
            _products.Add(meat);
        }
        private void ConsoleAddDairyProduct()
        {
            DairyProduct dairy = new DairyProduct();
            dairy.ConsoleSet();
            _products.Add(dairy);
        }



    }
}
