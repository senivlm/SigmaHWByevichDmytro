namespace Task11
{

    public interface IProduct : IComparable, ICloneable
    {
        double Price { get; set; }
        string Name { get; set; }
        void ChangePrice(int present);
    }
}
