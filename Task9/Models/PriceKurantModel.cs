using System.Collections.Generic;

namespace Task9
{
    internal class PriceKurantModel
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
        public bool TryGetProductPrice(string productName, out double price)
        {
            return _productPrice.TryGetValue(productName, out price);
        }
    }
}
