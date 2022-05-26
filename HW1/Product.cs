using System;


namespace ProductsProj
{
    internal class Product
    {
        #region Props

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _price;
        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Negative price");
                _price = value;
            }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Negative weight");
                _weight = value;
            }
        }
        #endregion


        public Product() : this("name", default, default) { }
        public Product(string name, double price, double weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }
        public virtual void ChangePrice(int persent)
        {
            Price += Price / 100d * persent;
        }
        public virtual void ConsoleSet()
        {
            Console.Write("Input name > ");
            Name = Console.ReadLine();
            Console.Write("Input price > ");
            double.TryParse(Console.ReadLine(), out _price);
            Console.Write("Input weight > ");
            double.TryParse(Console.ReadLine(), out _weight);
        }


        #region ObjectOverrides

        public override bool Equals(object? obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            return (obj is Product other &&
                    this.Name == other.Name &&
                    this.Price == other.Price &&
                    this.Weight == other.Weight);

        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string? ToString()
        {
            return $"Name: {Name}, Price: {Math.Round(Price,3)}, Weight: {Math.Round(Weight, 3)}, ";
        }
        #endregion
    }
}
