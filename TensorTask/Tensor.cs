using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorTask
{
    internal class Tensor<T>
    {
        private int _dimention;
        private List<TensorItem<T>> _tensorItems;
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

    }
}
