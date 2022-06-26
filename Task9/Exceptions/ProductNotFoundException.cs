using System;

namespace Task9.Exceptions
{
    internal class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base() { }
        public ProductNotFoundException(string message) : base(message) { }
    }
}
