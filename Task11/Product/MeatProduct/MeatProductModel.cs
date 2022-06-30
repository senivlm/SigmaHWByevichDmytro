using System;
using System.Collections.Generic;
using Task11.Enums;
using Task11.Product.General;

namespace Task11.Product
{
    internal class MeatProductModel : FoodProductBase, IMeatProduct
    {

        #region Props
        public MeatSpecies meatSpecies { get; set; }
        public MeatCategory meatCategory { get; set; }
        #endregion
        #region Ctors
        public MeatProductModel() :
            this(default, default, default, default, default, default, default)
        { }

        public MeatProductModel(string name, double price, double weight, DateTime expirationTime, MeatSpecies meatSpecies, MeatCategory meatCategory, SortedDictionary<int, int> daysToExpirationAndPresentOfChange)
        {
            Name = name;
            Price = price;
            Weight = weight;
            ExpirationTime = expirationTime;
            this.meatSpecies = meatSpecies;
            this.meatCategory = meatCategory;
            _daysToExpirationAndPresentOfChange = new SortedDictionary<int, int>();
            foreach (KeyValuePair<int, int> item in daysToExpirationAndPresentOfChange)
            {
                _daysToExpirationAndPresentOfChange.Add(item.Key, item.Value);
            }
        }

        public MeatProductModel(MeatProductModel other) :
            this(other.Name, other.Price, other.Weight, other.ExpirationTime, other.meatSpecies, other.meatCategory, other._daysToExpirationAndPresentOfChange)
        { }

        #endregion
        #region Methods
        public override object Clone()
        {
            return new MeatProductModel(this);
        }

        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            return base.ToString() + $"Вид м'яса: {meatSpecies}; Категорія м'яса: {meatCategory}";
        }

        #endregion


    }
}
