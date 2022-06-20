using System.Collections.Generic;

namespace Task9
{
    internal class Dish
    {
        private Dictionary<string, double> _ingridients;
        public double this[string key] => _ingridients[key];
        public int Length => _ingridients.Count;
        public IEnumerable<string> Keys => _ingridients.Keys;

        public Dish()
        {
            _ingridients = new();
        }
        public Dish(Dictionary<string, double> ingridients) : this()
        {
            _ingridients = ingridients;
        }
    }
}
