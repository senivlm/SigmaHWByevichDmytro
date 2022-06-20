using System;
using System.Collections.Generic;

namespace Task8_3
{
    internal class ProductPriceComparer : IComparer<Product>
    {
        public int Compare(Product x, Product y)
        {
            if (x is null || y is null)
            {
                throw (new ArgumentException("wrong object to compare"));
            }
            return ((x.Weight).CompareTo(y.Price));
        }
    }
}
