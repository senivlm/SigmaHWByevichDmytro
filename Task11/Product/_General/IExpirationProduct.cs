using System;

namespace Task11
{
    internal interface IExpirationProduct : IProduct
    {
        DateTime ExpirationTime { get; set; }
    }
}
