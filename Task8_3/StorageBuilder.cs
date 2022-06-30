using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task8_3
{
    internal class StorageBuilder
    {
        private List<Product> _products;
        public IEnumerable<Product> GetProducts()
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
            List<Option> options = new List<Option>()
            {
               new Option("Add meat product", () => ConsoleAddMeatProduct()),
               new Option("Add diary product", () => ConsoleAddDairyProduct())
            };
            Menu menu = new Menu(options);
            menu.PrintMenu();
            return this;
        }
        public StorageBuilder AddProductsFromFile(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                ValidateString(line);
            }
            return this;
        }
        public StorageBuilder AddProductsFromFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        ValidateString(line);
                    }
                    return this;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ValidateString(string line)
        {
            string[] productData = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!Enum.TryParse(productData[0], out ProductType productType))
            {
                throw new ArgumentException("Невідомий тип продукту");
            }
            try
            {
                switch (productType)
                {
                    case ProductType.product:
                        _products.Add(new Product(productData[1..]));
                        break;
                    case ProductType.meat:
                        _products.Add(new Meat(productData[1..]));
                        break;
                    case ProductType.dairy:
                        _products.Add(new DairyProduct(productData[1..]));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                StorageLogger.LogAppend(line);
            }
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
