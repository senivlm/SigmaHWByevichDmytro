using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TensorTask
{
    internal class TensorCube<T> : Tensor<T>
    {
        #region Props
        private int _sideSize;

        public int SideSize => _sideSize; 
        #endregion
        #region Ctors
        public TensorCube() : base(3)
        {
            _sideSize = 0;
        }
        public TensorCube(int sideSize) : base(3)
        {
            _sideSize = sideSize;
        }
        public TensorCube(int sideSize, IEnumerable<TensorItem<T>> tensorItems) : base(3, tensorItems)
        {
            _sideSize = sideSize;
        }
        public TensorCube(int sideSize, params TensorItem<T>[] tensorItems) : base(3, tensorItems)
        {
            _sideSize = sideSize;
        }
        #endregion
        #region Methods
        public bool IsHollowFromTo(int[] coord, Axis3D axisTo, int coordTo)
        {
            if (coord.Length != _dimention)
            {
                throw new ArgumentException();
            }
            return IsHollowFromTo(coord[0], coord[1], coord[2], axisTo, coordTo);
        }
        //TODO: Come up with better methods name
        public bool IsHollowFromTo(int xFrom, int yFrom, int zFrom, Axis3D axisTo, int coordTo)
        {
            if (coordTo >= _sideSize || coordTo < 0)
            {
                throw new IndexOutOfRangeException();
            }
            int startPos = 0;
            switch (axisTo)
            {
                case Axis3D.X:
                    startPos = xFrom;
                    break;
                case Axis3D.Y:
                    startPos = yFrom;
                    break;
                case Axis3D.Z:
                    startPos = zFrom;
                    break;
                default:
                    throw new ArgumentException();
            }
            if (startPos > coordTo)
            {
                (startPos, coordTo) = (coordTo, startPos);
            }
            switch (axisTo)
            {
                case Axis3D.X:
                    if (!TryPassToCoord(startPos, yFrom, zFrom, startPos, coordTo))
                    {
                        return false;
                    }
                    break;
                case Axis3D.Y:
                    if (!TryPassToCoord(xFrom, startPos, zFrom, startPos, coordTo))
                    {
                        return false;
                    }
                    break;
                case Axis3D.Z:

                    if (!TryPassToCoord(xFrom, yFrom, startPos, startPos, coordTo))
                    {
                        return false;
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
            return true;
        }
        private bool TryPassToCoord(int x, int y, int z, int startPos, int coordTo)
        {
            while (startPos < coordTo)
            {
                if (this[x, y, z].Value != null)
                {
                    return false;
                }
                startPos++;
            }
            return true;
        }
        #endregion
        #region Initialize
        public void InitializeWithSameValue(T value)
        {
            for (int i = 0; i < _sideSize; i++)
            {
                for (int j = 0; j < _sideSize; j++)
                {
                    for (int k = 0; k < _sideSize; k++)
                    {
                        AddItem(new TensorItem<T>(3, value, i, j, k));
                    }

                }
            }
        }
        #endregion
        #region Prints(temporary)
        //TODO: Remake to method(s), that create another 2D Tensors from outer sides
        // print it or return list of 2D Tensors
        public void PrintTensorAsCubeSides()
        {
            bool isNewLine = false;
            for (int k = 0; k < 6; k++)
            {
                switch (k)
                {
                    case 0:
                        Console.WriteLine("A,A1,B1,B");
                        break;
                    case 1:
                        Console.WriteLine("B,B1,C1,C");
                        break;
                    case 2:
                        Console.WriteLine("A,B,C,D");
                        break;
                    case 3:
                        Console.WriteLine("D,D1,C1,C");
                        break;
                    case 4:
                        Console.WriteLine("A,A1,D1,D");
                        break;
                    case 5:
                        Console.WriteLine("A1,B1,C1,D1");
                        break;
                    default:
                        break;
                }
                for (int i = 0; i < _sideSize; i++)
                {
                    for (int j = 0; j < _sideSize; j++)
                    {
                        if (this[i, j, 0] is not null)
                        {
                            switch (k)
                            {
                                case 0:
                                    Console.Write($"{this[0, i, j]}\t");
                                    break;
                                case 1:
                                    Console.Write($"{this[i, 0, j]}\t");
                                    break;
                                case 2:
                                    Console.Write($"{this[i, j, 0]}\t");
                                    break;
                                case 3:
                                    Console.Write($"{this[_sideSize - 1, i, j]}\t");
                                    break;
                                case 4:
                                    Console.Write($"{this[i, _sideSize - 1, j]}\t");
                                    break;
                                case 5:
                                    Console.Write($"{this[i, j, _sideSize - 1]}\t");
                                    break;
                                default:
                                    break;
                            }
                            isNewLine = true;
                        }
                    }
                    if (isNewLine)
                    {
                        Console.WriteLine();
                        isNewLine = false;
                    }
                }
            }
        }
        //TODO: maybe change it to ToString()
        public void PrintTensorAsCube()
        {
            bool isNewLine = false;
            for (int i = 0; i < _sideSize; i++)
            {
                for (int j = 0; j < _sideSize; j++)
                {
                    for (int k = 0; k < _sideSize; k++)
                    {
                        if (this[i, j, k] is not null)
                        {
                            Console.Write($"{this[i, j, k]}\t");
                            isNewLine = true;
                        }
                    }
                    if (isNewLine)
                    {
                        Console.WriteLine();
                        isNewLine = false;
                    }
                }
            }
        } 
        #endregion
    }
}
