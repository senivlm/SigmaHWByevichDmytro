using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    internal class Storage : IFileReader
    {
        private List<Product> _products;
        public int Count { get => _products.Count; }
        public Product this[int index]
        {
            get { return _products[index]; }
            set { _products[index] = value; }
        }
        public Storage()
        {
            _products = new List<Product>();
        }
        public Storage(StorageBuilder storageBuilder)
        {
            _products = new List<Product>(storageBuilder.GetProducts());
        }
        public Storage(IEnumerable<Product> products)
        {
            _products = new List<Product>(products);
        }
        public Storage(FileHandler storageFile)
        {
            _products = new List<Product>();
            storageFile.ReadToObject(this);
        }

        public void ReadFromStream(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                ValidateString(line);

            }
        }
        private void ValidateString(string line)
        {
            string[] productData = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!Enum.TryParse(productData[0], out ProductType productType))
            {
                throw new ArgumentException("Невідомий тип продукту");
            }
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
        public IEnumerable<Product> GetMeatProducts()
        {
            List<Product> meatProducts = new List<Product>();
            meatProducts = _products.Where(x => x is Meat).ToList();
            return meatProducts;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var product in _products)
            {
                stringBuilder.AppendLine(product.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
