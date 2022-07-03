using System;
using System.Collections.Generic;

namespace Task11
{
    internal interface IExpirationProduct : IProduct
    {
        DateTime ExpirationTime { get; set; }
        SortedDictionary<int, int> DaysToExpirationAndPresentOfChange { get; }
        double GetPriceByExpiration();

    }
}
