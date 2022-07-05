using Task11.Enums;

namespace Task11
{
    internal interface IMeatProduct : IFoodProduct
    {
        MeatSpecies MeatSpeciesProp { get; set; }
        MeatCategory MeatCategoryProp { get; set; }
    }
}
