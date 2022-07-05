using System;

namespace Task11.Product._General
{
    internal interface IDurationProduct : IProduct
    {
        TimeSpan Duration { get; set; }
    }
}
