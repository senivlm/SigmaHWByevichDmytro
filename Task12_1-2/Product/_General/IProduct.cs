using System;

namespace Task11
{
    internal interface IProduct : IComparable, ICloneable
    {
        string Name { get; set; }
        double Price { get; set; }
        void ChangePrice(int present);
    }
}
