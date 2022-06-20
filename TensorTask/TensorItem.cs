using System;
using System.Text;

namespace TensorTask
{
    internal class TensorItem<T>
    {
        #region Props
        private T _value;
        private int[] _coords;
        private int _dimention;
        public int[] Coord => _coords[..];
        public int Dimention => _dimention;

        public T Value
        {
            get => _value;
            set => _value = value;
        }
        #endregion
        #region Ctors
        public TensorItem()
        {
            _dimention = 0;
            _coords = Array.Empty<int>();
            _value = default;
        }
        public TensorItem(int dimention, T value, params int[] coords)
        {

            if (dimention < 0)
            {
                throw new ArgumentException("Від'ємна розмірність");
            }
            if (coords.Length != dimention)
            {
                throw new ArgumentException("Невірна розмірність координат");
            }
            _value = value;
            _dimention = dimention;
            _coords = new int[_dimention];
            _coords = coords[..];
        }
        #endregion
        #region Methods
        public bool CoordEquals(TensorItem<T> other)
        {
            if (_dimention != other._dimention)
            {
                return false;
            }
            for (int i = 0; i < _coords.Length; i++)
            {
                if (_coords[i] != other._coords[i])
                {
                    return false;
                }
            }
            return true;
        }
        public bool CoordEquals(int[] coord)
        {
            if (_dimention != coord.Length)
            {
                return false;
            }
            for (int i = 0; i < _coords.Length; i++)
            {
                if (_coords[i] != coord[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            if (obj is null || obj is not TensorItem<T> other)
            {
                return false;
            }
            return CoordEquals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            foreach (int coord in _coords)
            {
                stringBuilder.Append(string.Format("{0:f2}", coord + " "));
            }
            stringBuilder.Append("}");
            stringBuilder.Append($":{_value}; ");
            return stringBuilder.ToString();

        }
        #endregion
    }
}
