using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson14_06
{
    internal class Dish
    {
        private Dictionary<string, double> _ingridients;
        public double this[string key]
        {
            get
            {
                return _ingridients[key];
            }
        }
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
