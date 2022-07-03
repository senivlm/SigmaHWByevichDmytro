using System;
using System.Collections.Generic;
using System.Text;
using Task11.Enums;
using Task11.FileHandler;
using Task11.Product.General;

namespace Task11.Product
{
    internal class MeatProductModel : FoodProductBase, IMeatProduct, ITXTSerializer
    {

        #region Props
        public MeatSpecies MeatSpeciesProp { get; set; }
        public MeatCategory MeatCategoryProp { get; set; }
        #endregion
        #region Ctors
        public MeatProductModel() :
            this(default, default, default, default, default, default, default)
        { }

        public MeatProductModel(string name, double price, double weight, DateTime expirationTime, MeatSpecies meatSpecies, MeatCategory meatCategory, SortedDictionary<int, int> daysToExpirationAndPresentOfChange) :
            base(name, price, weight, expirationTime, daysToExpirationAndPresentOfChange)
        {
            MeatSpeciesProp = meatSpecies;
            MeatCategoryProp = meatCategory;
        }

        public MeatProductModel(MeatProductModel other) :
            this(other.Name, other.Price, other.Weight, other.ExpirationTime, other.MeatSpeciesProp, other.MeatCategoryProp, other._daysToExpirationAndPresentOfChange)
        { }

        #endregion
        #region Methods
        public override object Clone()
        {
            return new MeatProductModel(this);
        }
        public string SerializeTxt()
        {
            StringBuilder sb = new();
            sb.Append('{');
            foreach (KeyValuePair<int, int> item in _daysToExpirationAndPresentOfChange)
            {
                sb.Append($"({item.Key} {item.Value})");
            }
            sb.Append('}');
            return $"<MeatProduct>;<Name: {Name}>;<Price: {Price}>;<Weight: {Weight}>;<ExpirationTime: {ExpirationTime:d}>;<MeatSpecies: {MeatSpeciesProp}>;<MeatCategoryProp: {MeatCategoryProp}>;<DaysToExpirationAndPresentOfChange: {sb}>;";
        }

        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            return base.ToString() + $"Вид м'яса: {MeatSpeciesProp}; Категорія м'яса: {MeatCategoryProp}";
        }

        

        #endregion


    }
}
