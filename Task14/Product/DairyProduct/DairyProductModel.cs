﻿using Task11.Product.General;

namespace Task11.Product
{
    [Serializable]
    public class DairyProductModel : FoodProductBase, IDairyProduct
    {
        #region Props

        #endregion
        #region Ctors
        public DairyProductModel() :
            this(default, default, default, default, default)

        { }

        public DairyProductModel(string name, double price, double weight, DateTime expirationTime, SortedDictionary<int, int> daysToExpirationAndPresentOfChange) :
            base(name, price, weight, expirationTime, daysToExpirationAndPresentOfChange)
        { }

        public DairyProductModel(DairyProductModel other) :
            this(other.Name, other.Price, other.Weight, other.ExpirationTime, other._daysToExpirationAndPresentOfChange)
        { }
        #endregion
        #region Methods
        public override object Clone()
        {
            return new DairyProductModel(this);
        }
        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }
}
