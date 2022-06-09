using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    internal class DairyProduct : Product
    {
        #region Props
        private int _expirationDays;

        public int ExpirationDays
        {
            get { return _expirationDays; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Invalid expiration");
                _expirationDays = value;
            }
        }
        #endregion

        public DairyProduct() : this("name", default, default, 1) { }

        public DairyProduct(string name, double price, double weight, int expirationDays) : base(name, price, weight)
        {
            ExpirationDays = expirationDays;
        }

        public DairyProduct(params string[] productData)
        {
            ValidateParams(productData);
        }
        protected override void ValidateParams(string[] productData)
        {
            if (productData.Length != 4)
            {
                throw new ArgumentException("Невірна кількість записів");
            }
            base.ValidateParams(productData[..3]);
            if (!int.TryParse(productData[3], out int expirationDays))
            {
                throw new ArgumentException("Невірний формат терміну придатності");
            }
            ExpirationDays = expirationDays;
        }

        public override void ConsoleSet()
        {
            base.ConsoleSet();
            Console.Write("Input expiration days > ");
            int.TryParse(Console.ReadLine(), out _expirationDays);
        }
        public override void ChangePrice(int persent)
        {
            Price += Price / 100d * persent;
            if (_expirationDays < 2)
            {
                Price += Price / 100d * -90;
            }
            else if (_expirationDays < 4)
            {
                Price += Price / 100d * -70;
            }
            else if (_expirationDays < 5)
            {
                Price += Price / 100d * -50;
            }
            else if (_expirationDays < 7)
            {
                Price += Price / 100d * -20;
            }

        }

        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return (obj is DairyProduct other &&
                    Equals((this as Product), (other as Product)) &&
                    ExpirationDays == other.ExpirationDays);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString() + $" {ExpirationDays}";
        }


        #endregion
    }
}
