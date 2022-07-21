using Task14.Enums;

namespace Task14
{
    public interface IMeatProduct : IFoodProduct
    {
        MeatSpecies MeatSpeciesProp { get; set; }
        MeatCategory MeatCategoryProp { get; set; }
    }
}
