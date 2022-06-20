using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task7
{
    internal class Storage : IFileReader, IFileWriter
    {
        private List<Product> _products;
        public int Count => _products.Count;
        public Product this[int index]
        {
            get => _products[index];
            set => _products[index] = value;
        }
        public IEnumerable<Product> this[Range range] => _products.GetRange(range.Start.Value, range.GetOffsetAndLength(_products.Count).Length);
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
            _products = new List<Product>(new StorageBuilder().AddProductsFromFile(reader)
                                                              .GetProducts());
        }

        public void ChangePrice(int precent)
        {
            foreach (Product product in _products)
            {
                product.ChangePrice(precent);
            }
        }
        public void PrintProducts()
        {
            Console.WriteLine("\nProducts storage:");
            foreach (Product product in _products)
            {
                Console.WriteLine(product);
            }
        }
        public IEnumerable<Product> GetMeatProducts()
        {
            List<Product> meatProducts = new List<Product>();
            meatProducts = _products.Where(x => x is Meat).ToList();
            return meatProducts;
        }
        public bool TryAddProductFromLine(string line)
        {
            string[] productData = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!Enum.TryParse(productData[0], out ProductType productType))
            {
                return false;
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
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!_products.Any())
            {
                return null;
            }
            foreach (var product in _products)
            {
                stringBuilder.AppendLine(product.ToString());
            }
            return stringBuilder.ToString();
        }

        public void WriteToStream(StreamWriter writer, bool append = false)
        {
            foreach (var product in _products)
            {
                if (product is Meat)
                {
                    writer.WriteLine("meat" + " " + product.ToString());

                }
                else if (product is DairyProduct)
                {
                    writer.WriteLine("dairy" + " " + product.ToString());

                }
                else if (product is Product)
                {
                    writer.WriteLine("product" + " " + product.ToString());

                }

            }
        }
    }
}
