using System;

namespace Task11
{
    internal interface IProduct : IComparable, ICloneable
    {
        double Price { get; set; }
        string Name { get; set; }
        void ChangePrice(int present);
    }
}
