using System;

namespace Task11.Services
{
    internal static class PreAddBehaviorService
    {
        public static bool IsProductNotExpired<T>(T product) where T : IProduct
        {
            return (product is not IExpirationProduct expirationProduct || DateTime.Now < expirationProduct.ExpirationTime);
        }
    }
}
