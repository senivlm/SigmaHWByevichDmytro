using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Task12_3
{
    internal class Operations : IDictionary<string, IOperation>, IList<IOperation>
    {
        private Dictionary<string, IOperation> _operations;
        public Operations()
        {
            _operations = new();
        }
        public Operations(IEnumerable<IOperation> operations) : this()
        {
            foreach (IOperation item in operations)
            {
                _operations.Add(item.Name, item);
            }
        }

        #region IDictionary
        public IOperation this[string key]
        {
            get => _operations[key];
            set => _operations[key] = value;
        }

        public ICollection<string> Keys => _operations.Keys;

        public ICollection<IOperation> Values => _operations.Values;

        public int Count => _operations.Count;

        public bool IsReadOnly => false;

        public void Add(string key, IOperation value)
        {
            _operations.Add(key, value);
        }

        public void Add(KeyValuePair<string, IOperation> item)
        {
            _operations.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _operations.Clear();
        }
        public bool Contains(KeyValuePair<string, IOperation> item)
        {
            return _operations.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return _operations.ContainsKey(key);
        }
        public void CopyTo(KeyValuePair<string, IOperation>[] array, int arrayIndex)
        {
            Array.Copy(_operations.ToArray(), 0, array, arrayIndex, _operations.Count);
        }

        public IEnumerator<KeyValuePair<string, IOperation>> GetEnumerator()
        {
            return _operations.GetEnumerator();
        }
        public bool Remove(string key)
        {
            return _operations.Remove(key);
        }

        public bool Remove(KeyValuePair<string, IOperation> item)
        {
            return _operations.Remove(item.Key);
        }
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out IOperation value)
        {
            return _operations.TryGetValue(key, out value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region IList
        public IOperation this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(IOperation item)
        {
            _operations.Add(item.Name, item);
        }
        public bool Contains(IOperation item)
        {
            return ContainsKey(item.Name);
        }
        public void CopyTo(IOperation[] array, int arrayIndex)
        {
            Array.Copy(_operations.ToArray(), 0, array, arrayIndex, _operations.Count);
        }
        public int IndexOf(IOperation item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IOperation item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IOperation item)
        {
            return Remove(item.Name);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator<IOperation> IEnumerable<IOperation>.GetEnumerator()
        {
            foreach (KeyValuePair<string, IOperation> item in _operations)
            {
                yield return item.Value;
            }

        }
        #endregion

    }
}
