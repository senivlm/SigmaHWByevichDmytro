using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsProj
{
    internal class Meat : Product
    {
        MeatSpecies _species;

        public MeatSpecies Species
        {
            get => _species;
            set
            {
                _species = value;
            }
        }
        ProductCategory _category;
        public ProductCategory Category
        {
            get => _category;
            set
            {
                _category = value;
            }

        }


        public Meat() : this("name", default, default, MeatSpecies.None, ProductCategory.None) { }
        public Meat(string name, double price, double weight, MeatSpecies species, ProductCategory category) : base(name, price, weight)
        {
            Species = species;
            Category = category;
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

            switch (Category)
            {
                case ProductCategory.None:
                    throw new InvalidOperationException("None ProductCategory");
                case ProductCategory.First:
                    Price += Price / 100d * 30;
                    Price += Price / 100d * persent;
                    break;
                case ProductCategory.Second:
                    Price += Price / 100d * 20;
                    Price += Price / 100d * persent;
                    break;
                case ProductCategory.Third:
                    Price += Price / 100d * 10;
                    Price += Price / 100d * persent;
                    break;
                default:
                    throw new InvalidOperationException("Error ProductCategory");
            }
        }

        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
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
            return base.ToString() + $"Species: {Species}, Category: {Category} ";
        } 
        #endregion
    }
}

