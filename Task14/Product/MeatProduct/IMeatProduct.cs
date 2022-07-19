using Task11.Enums;

namespace Task11
{
    public interface IMeatProduct : IFoodProduct
    {
        MeatSpecies MeatSpeciesProp { get; set; }
        MeatCategory MeatCategoryProp { get; set; }
    }
}
