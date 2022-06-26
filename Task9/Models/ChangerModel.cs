using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Task9.FIleHandler;

namespace Task9.Models
{
    internal class ChangerModel : IEnumerable, ITxtSerializer
    {
        private Dictionary<string, double> _changer;



        public IEnumerator GetEnumerator()
        {
            return _changer.GetEnumerator();
        }

        public ChangerModel()
        {
            _changer = new();
        }
        public ChangerModel(Dictionary<string, double> changePrices) : this()
        {
            _changer = changePrices;
        }
        public bool ContainsCurrency(string currency)
        {
            return _changer.ContainsKey(currency);
        }

        public bool TryGetValue(string key, out double value)
        {
            return _changer.TryGetValue(key, out value);
        }

        public void Add(string name, double price)
        {
            if (_changer.ContainsKey(name))
            {
                throw new Exception("Така пара вже існує");
            }
            _changer.Add(name, price);
        }
        public string SerializeTxt()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, double> changePair in _changer)
            {
                stringBuilder.AppendLine($"{changePair.Key} {changePair.Value}");
            }
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<string, double> changePair in _changer)
            {
                stringBuilder.AppendLine($"Валюта: {changePair.Key}; Ціна у грн {changePair.Value}");
            }
            return stringBuilder.ToString();
        }
    }
}
