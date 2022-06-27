using System.Collections;
using System.Collections.Generic;
using System.Text;
using Task9.FIleHandler;

namespace Task9
{
    internal class DishModel : IEnumerable, ITxtSerializer
    {

        private Dictionary<string, double> _ingridients;
        public string Name { get; set; }
        public double this[string key] => _ingridients[key];
        public int Length => _ingridients.Count;
        public IEnumerable<string> Keys => _ingridients.Keys;
        public IEnumerable<double> Values => _ingridients.Values;

        public DishModel()
        {
            _ingridients = new();
        }
        public DishModel(Dictionary<string, double> ingridients, string name) : this()
        {
            Name = name;
            foreach (var item in ingridients)
            {
                _ingridients.TryAdd(item.Key, item.Value);
            }
        }

        public DishModel(DishModel dish) : this(dish._ingridients, dish.Name) { }

        public bool TryAddIngridient(string name, double weight)
        {
            return _ingridients.TryAdd(name, weight);
        }

        public IEnumerator GetEnumerator()
        {
            return _ingridients.GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Name);
            foreach (KeyValuePair<string, double> ing in _ingridients)
            {
                stringBuilder.AppendLine($"Продукт: {ing.Key}; Вага: {ing.Value}");
            }
            return stringBuilder.ToString();
        }

        public string SerializeTxt()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(Name);
            foreach (KeyValuePair<string, double> ing in _ingridients)
            {
                stringBuilder.AppendLine($"{ing.Key} {ing.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}
