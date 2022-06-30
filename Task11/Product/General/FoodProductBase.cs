using System;
using System.Collections.Generic;

namespace Task11.Product.General
{
    internal abstract class FoodProductBase : IFoodProduct
    {

        #region Props

        protected SortedDictionary<int, int> _daysToExpirationAndPresentOfChange;

        public DateTime ExpirationTime { get; set; }

        protected double _weight;
        public double Weight
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

        protected double _price;
        public double Price
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
        public string Name { get; set; }
        #endregion


        #region Methods
        public abstract object Clone();
        public virtual void ChangePrice(int present)
        {
            Price += _price / 100d * present;
        }
        public virtual int CompareTo(object obj)
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
            return $"Назва: {Name}; Ціна: {Price}; Вага: {Weight} Термін придатності: {ExpirationTime.Date:d}; ";
        }

        #endregion


    }
}
