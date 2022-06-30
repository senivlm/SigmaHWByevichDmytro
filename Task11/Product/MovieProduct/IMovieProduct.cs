namespace Task11.Product.General
{
    internal interface IMovieProduct : IDurationDigitalProduct, IAuthorProduct
    {
        string Genre { get; set; }
    }
}
