using Task11.Enums;

namespace Task11
{
    internal interface IMeatProduct : IFoodProduct
    {
        MeatSpecies meatSpecies { get; set; }
        MeatCategory meatCategory { get; set; }
    }
}
