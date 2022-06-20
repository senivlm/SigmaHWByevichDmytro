using System;

namespace Task7
{
    internal class Meat : Product
    {
        private MeatSpecies _species;

        public MeatSpecies Species
        {
            get => _species;
            set => _species = value;
        }

        private MeatCategory _category;
        public MeatCategory Category
        {
            get => _category;
            set => _category = value;

        }


        public Meat() : this("name", default, default, MeatSpecies.None, MeatCategory.None) { }
        public Meat(string name, double price, double weight, MeatSpecies species, MeatCategory category) : base(name, price, weight)
        {
            Species = species;
            Category = category;
        }

        public Meat(params string[] productData)
        {
            ValidateParams(productData);
        }

        public override void ConsoleSet()
        {
            base.ConsoleSet();
            Console.Write("Input meat species > ");
            Enum.TryParse(Console.ReadLine(), out _species);
            Console.Write("Input category > ");
            Enum.TryParse(Console.ReadLine(), out _category);
        }
        public override void ChangePrice(int persent)
        {
            Price += Price / 100d * persent;
            switch (Category)
            {
                case MeatCategory.None:
                    throw new InvalidOperationException("None ProductCategory");
                case MeatCategory.First:
                    Price += Price / 100d * 30;
                    break;
                case MeatCategory.Second:
                    Price += Price / 100d * 20;
                    break;
                case MeatCategory.Third:
                    Price += Price / 100d * 10;
                    break;
                default:
                    throw new InvalidOperationException("Error ProductCategory");
            }
        }
        protected override void ValidateParams(string[] productData)
        {
            if (productData.Length != 5)
            {
                throw new ArgumentException("Невірна кількість записів");
            }
            base.ValidateParams(productData[..3]);
            if (!Enum.TryParse(productData[3], out _species))
            {
                throw new ArgumentException("Невідомий вид м'яса");
            }
            if (!Enum.TryParse(productData[4], out _category))
            {
                throw new ArgumentException("Невідома категорія");
            }

        }

        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            return (obj is Meat other &&
                    Equals((this as Product), (other as Product)) &&
                    Category == other.Category &&
                    Species == other.Species);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString() + $" {Species} {Category}";
        }

        #endregion
    }
}

