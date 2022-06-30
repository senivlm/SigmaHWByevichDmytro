using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task11
{
    internal class ProductStorage<T> : IList<T>
        where T : IProduct
    {
        #region Props
        private List<T> _products;
        #endregion
        #region Ctors
        public ProductStorage()
        {
            _products = new List<T>();
        }
        public ProductStorage(IEnumerable<T> products) : this()
        {
            foreach (T product in products)
            {
                _products.Add((T)product.Clone());
            }
        }
        #endregion
        #region IList
        public T this[int index]
        {
            get => _products[index];
            set => _products[index] = (T)value.Clone();
        }

        public int Count => _products.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _products.Add(item);
        }

        public void Clear()
        {
            _products.Clear();
        }

        public bool Contains(T item)
        {
            return _products.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _products.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _products.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _products.GetEnumerator();
        }
        public int IndexOf(T item)
        {
            return _products.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _products.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return (_products.Remove(item));
        }

        public void RemoveAt(int index)
        {
            _products.RemoveAt(index);
        }

        #endregion
        #region Methods
        public IEnumerable<G> GetAll<G>() where G : IProduct
        {
            foreach (T item in _products)
            {
                if (item is G result)
                {
                    yield return result;
                }
            }
        }

        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (IProduct product in _products)
            {
                sb.AppendLine(product.ToString());
            }
            return sb.ToString();
        }

        #endregion

    }
}
