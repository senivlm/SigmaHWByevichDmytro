using System;
using System.Collections.Generic;
using Task11.Product._General;

namespace Task11.Product.General
{
    internal abstract class FoodProductBase : ProductBase, IFoodProduct
    {

        #region Props

        protected double _weight;
        protected SortedDictionary<int, int> _daysToExpirationAndPresentOfChange;
        public virtual DateTime ExpirationTime { get; set; }
        public virtual double Weight
        {
            get => _weight;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                _weight = value;
            }
        }
        public override double Price
        {
            get => GetPriceByExpiration();
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }
                _price = value;
            }
        }

        #endregion
        #region Methods
        public override int CompareTo(object obj)
        {
            IFoodProduct other = obj as IFoodProduct;
            if (other is null)
            {
                throw (new ArgumentException("wrong object to compare"));
            }
            return ((other.Price * other.Weight).CompareTo(Price * Weight));
        }
        protected virtual double GetPriceByExpiration()
        {
            int daysToExpiretion = ExpirationTime.Subtract(DateTime.Today).Days;
            foreach (var item in _daysToExpirationAndPresentOfChange)
            {
                if (daysToExpiretion <= item.Key)
                {
                    return _price - _price * item.Value / 100d;
                }
            }
            return _price;
        }
        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            return base.ToString()+$"Вага: {Weight} Термін придатності: {ExpirationTime.Date:d}; ";
        }

        #endregion


    }
}
