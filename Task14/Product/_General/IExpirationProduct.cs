namespace Task11
{
    public interface IExpirationProduct : IProduct
    {
        DateTime ExpirationTime { get; set; }
        SortedDictionary<int, int> DaysToExpirationAndPresentOfChange { get; set; }
        double GetPriceByExpiration();

    }
}
