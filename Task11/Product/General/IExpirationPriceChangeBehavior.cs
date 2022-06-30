using System;

namespace Task11.Product.General
{
    internal interface IExpirationPriceChangeBehavior
    {
        double GetPriceByExpiration(DateTime ExpirationTime);
    }
}
