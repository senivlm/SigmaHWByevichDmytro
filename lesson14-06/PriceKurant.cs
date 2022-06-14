using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson14_06
{
    internal class PriceKurant
    {
        private Dictionary<string, double> _productPrice;
        public PriceKurant()
        {
            _productPrice = new();
        }
        public PriceKurant(Dictionary<string, double> productPrice) : this()
        {
            _productPrice = productPrice;
        }
        public bool TryGetProductPrice(string productName, out double price)
        {
            if (!_productPrice.TryGetValue(productName, out double result))
            {             
                price = default;  
                return false;
            }
            price = result;
            return true;
        }
    }
}
