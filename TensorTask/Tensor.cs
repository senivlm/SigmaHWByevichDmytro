using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TensorTask
{
    internal class Tensor<T>
    {//Треба обговорити
        #region Props
        protected int _dimention;
        protected List<TensorItem<T>> _tensorItems;
        public int Dimention => _dimention;
        public int Length => _tensorItems.Count;
        public TensorItem<T> this[params int[] coord]
        {
            get
            {
                if (coord.Length != _dimention)
                {
                    throw new IndexOutOfRangeException();
                }
                return _tensorItems.FirstOrDefault(item => item.CoordEquals(coord));
            }
        }
        #endregion
        #region Ctors
        public Tensor()
        {
            _tensorItems = new();
            _dimention = 0;
        }

        public Tensor(int dimention)
        {
            _dimention = dimention;
            _tensorItems = new();
        }
        public Tensor(int dimention, IEnumerable<TensorItem<T>> tensorItems)
        {
            _dimention = dimention;
            _tensorItems = new(tensorItems.Where(item => item.Dimention == _dimention));
        }
        public Tensor(int dimention, params TensorItem<T>[] tensorItems)
        {
            _dimention = dimention;
            _tensorItems = new(tensorItems.Where(item => item.Dimention == _dimention));
        }
        #endregion
        #region Methods
        public void AddItem(TensorItem<T> tensorItem)
        {
            if (tensorItem.Dimention != _dimention)
            {
                throw new ArgumentException("Вимірнійсть об'єкта не відповідає вимірності тензора ");
            }
            if (_tensorItems.FirstOrDefault(item => item.Coord == tensorItem.Coord) != null)
            {
                _tensorItems[_tensorItems.IndexOf(_tensorItems.First(item => item.CoordEquals(tensorItem.Coord)))].Value = tensorItem.Value;
                return;
            }
            _tensorItems.Add(tensorItem);
        }
        #endregion
        #region Operators

        public static Tensor<T> operator +(Tensor<T> a, Tensor<T> b)
        {
            if (a._dimention != b._dimention)
            {
                throw new ArgumentException("Different dimentions ");
            }
            if (a.Length < b.Length)
            {
                (a, b) = (b, a);
            }
            Tensor<T> tensor = new(a._dimention, a._tensorItems);
            foreach (TensorItem<T> item in b._tensorItems)
            {
                if (!tensor._tensorItems.Contains(item))
                {
                    tensor._tensorItems.Add(item);
                }
            }
            return tensor;
        }
        #endregion
        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (TensorItem<T> item in _tensorItems)
            {
                stringBuilder.AppendLine(item.ToString());
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
