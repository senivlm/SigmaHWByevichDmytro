using System;

namespace Task11.Product.General
{
    internal interface IDurationDigitalProduct : IDigitalProduct
    {
        TimeSpan Duration { get; set; }
    }
}
