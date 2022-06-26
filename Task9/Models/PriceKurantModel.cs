using System;
using System.Collections.Generic;
using System.Text;
using Task9.FIleHandler;

namespace Task9
{
    internal class PriceKurantModel : ITxtSerializer
    {
        private Dictionary<string, double> _productPrice;
        public PriceKurantModel()
        {
            _productPrice = new();
        }
        public PriceKurantModel(Dictionary<string, double> productPrice) : this()
        {
            _productPrice = productPrice;
        }
        public bool ContainsKey(string key)
        {
            return _productPrice.ContainsKey(key);
        }

        public bool TryGetProductPrice(string productName, out double price)
        {
            return _productPrice.TryGetValue(productName, out price);
        }
        public void Add(string name, double price)
        {
            if (_productPrice.ContainsKey(name))
            {
                throw new Exception("Такий продукт вже існує");
            }
            _productPrice.Add(name, price);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, double> productPrice in _productPrice)
            {
                stringBuilder.AppendLine($"Продукт: {productPrice.Key}; Ціна {productPrice.Value}");
            }
            return stringBuilder.ToString();
        }

        public string SerializeTxt()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, double> productPrice in _productPrice)
            {
                stringBuilder.AppendLine($"{productPrice.Key} - {productPrice.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}
