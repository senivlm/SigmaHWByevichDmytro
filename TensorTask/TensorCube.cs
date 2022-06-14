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
        public Tensor<int> GetMatrixOfHollow(CubeSides cubeSide)
        {
            Tensor<int> hollowMatrix = new Tensor<int>(2);
            for (int i = 0; i < _sideSize; i++)
            {
                for (int j = 0; j < _sideSize; j++)
                {
                    int[] sideCoord = null;
                    Axis3D axisTo = Axis3D.NONE;
                    int coordTo = 0;
                    switch (cubeSide)
                    {
                        case CubeSides.UP:
                            sideCoord = new int[] { i, j, SideSize - 1 };
                            axisTo = Axis3D.Z;
                            break;
                        case CubeSides.FRONT:
                            sideCoord = new int[] { i, SideSize - 1, j };
                            axisTo = Axis3D.Y;
                            break;
                        case CubeSides.RIGHT:
                            sideCoord = new int[] { SideSize - 1, i, j };
                            axisTo = Axis3D.X;
                            break;
                        case CubeSides.BOTTOM:
                            sideCoord = new int[] { i, j, 0 };
                            axisTo = Axis3D.Z;
                            coordTo = SideSize - 1;
                            break;
                        case CubeSides.BACK:
                            sideCoord = new int[] { i, 0, j };
                            axisTo = Axis3D.Y;
                            coordTo = SideSize - 1;
                            break;
                        case CubeSides.LEFT:
                            sideCoord = new int[] { 0, i, j };
                            axisTo = Axis3D.X;
                            coordTo = SideSize - 1;
                            break;
                        default:
                            throw new ArgumentException();
                    }
                    if (this[sideCoord].Value is null && IsHollowFromTo(sideCoord, axisTo, coordTo))
                    {
                        hollowMatrix.AddItem(new TensorItem<int>(2, 1, i, j));
                        continue;
                    }
                    hollowMatrix.AddItem(new TensorItem<int>(2, 0, i, j));

                }
            }
            return hollowMatrix;
        }
        public bool IsHollowFromTo(int[] coord, Axis3D axisTo, int coordTo)
        {
            if (coord.Length != _dimention)
            {
                throw new ArgumentException();
            }
            return IsHollowFromTo(coord[0], coord[1], coord[2], axisTo, coordTo);
        }
        public bool IsHollowFromTo(int xFrom, int yFrom, int zFrom, Axis3D axisTo, int coordTo)
        {
            if (coordTo >= _sideSize || coordTo < 0)
            {
                throw new IndexOutOfRangeException();
            }
            switch (axisTo)
            {
                case Axis3D.X:
                    if (xFrom > coordTo)
                    {
                        (xFrom, coordTo) = (coordTo, xFrom);
                    }
                    break;
                case Axis3D.Y:
                    if (yFrom > coordTo)
                    {
                        (yFrom, coordTo) = (coordTo, yFrom);
                    }
                    break;
                case Axis3D.Z:
                    if (zFrom > coordTo)
                    {
                        (zFrom, coordTo) = (coordTo, zFrom);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }

            return TryPassToCoord(xFrom, yFrom, zFrom, axisTo, coordTo);

        }
        private bool TryPassToCoord(int x, int y, int z, Axis3D startPos, int coordTo)
        {
            switch (startPos)
            {
                case Axis3D.X:
                    while (x <= coordTo)
                    {
                        if (this[x, y, z].Value != null )
                        {
                            return false;
                        }
                        x++;
                    }
                    break;
                case Axis3D.Y:
                    while (y <= coordTo)
                    {
                        if (this[x, y, z].Value != null)
                        {
                            return false;
                        }
                        y++;
                    }
                    break;
                case Axis3D.Z:
                    while (z <= coordTo)
                    {
                        if (this[x, y, z].Value != null)
                        {
                            return false;
                        }
                        z++;
                    }
                    break;
                default:
                    break;
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
        public void InitializeWithSameValueAndRandomHollows(T value, int frequencyOfHollow)
        {
            var rand = new Random();
            for (int i = 0; i < _sideSize; i++)
            {
                for (int j = 0; j < _sideSize; j++)
                {
                    for (int k = 0; k < _sideSize; k++)
                    {
                        if (rand.Next(0, frequencyOfHollow) == 0)
                        {
                            AddItem(new TensorItem<T>(3, default, i, j, k));
                            continue;
                        }
                        AddItem(new TensorItem<T>(3, value, i, j, k));
                    }

                }
            }
        }
        #endregion
        #region ToStrings
        public string ToStringCubeSides()
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool isNewLine = false;
            for (int k = 0; k < 6; k++)
            {
                CubeSides cubeSide = (CubeSides)k;
                stringBuilder.AppendLine(cubeSide.ToString());

                for (int i = 0; i < _sideSize; i++)
                {
                    for (int j = 0; j < _sideSize; j++)
                    {
                        switch (cubeSide)
                        {
                            case CubeSides.FRONT:
                                stringBuilder.Append($"{this[0, i, j]}\t");
                                break;
                            case CubeSides.UP:
                                stringBuilder.Append($"{this[i, 0, j]}\t");
                                break;
                            case CubeSides.RIGHT:
                                stringBuilder.Append($"{this[i, j, 0]}\t");
                                break;
                            case CubeSides.BOTTOM:
                                stringBuilder.Append($"{this[_sideSize - 1, i, j]}\t");
                                break;
                            case CubeSides.BACK:
                                stringBuilder.Append($"{this[i, _sideSize - 1, j]}\t");
                                break;
                            case CubeSides.LEFT:
                                stringBuilder.Append($"{this[i, j, _sideSize - 1]}\t");
                                break;
                            default:
                                break;
                        }
                        isNewLine = true;
                    }
                    if (isNewLine)
                    {
                        stringBuilder.AppendLine();
                        isNewLine = false;
                    }
                }
            }
            return stringBuilder.ToString();
        }
        public string ToStringAsCube()
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool isNewLine = false;
            for (int i = 0; i < _sideSize; i++)
            {
                for (int j = 0; j < _sideSize; j++)
                {
                    for (int k = 0; k < _sideSize; k++)
                    {
                        if (this[i, j, k] is not null)
                        {
                            stringBuilder.Append($"{this[i, j, k]}\t");
                            isNewLine = true;
                        }
                    }
                    if (isNewLine)
                    {
                        stringBuilder.AppendLine();
                        isNewLine = false;
                    }
                }
            }
            return stringBuilder.ToString();
        }
        public string ToStringAllSidesHollows()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int k = 0; k < 6; k++)
            {
                CubeSides cubeSide = (CubeSides)k;
                var matrixOfHollow = this.GetMatrixOfHollow(cubeSide);
                stringBuilder.AppendLine($"Matrix of {cubeSide} side hollows: ");
                for (int i = 0; i < this.SideSize; i++)
                {
                    for (int j = 0; j < this.SideSize; j++)
                    {
                        stringBuilder.Append(matrixOfHollow[i, j].ToString());

                    }
                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
        }
        public void PrintAllSidesHollowsToConsole()
        {
            for (int k = 0; k < 6; k++)
            {
                CubeSides cubeSide = (CubeSides)k;
                var matrixOfHollow = this.GetMatrixOfHollow(cubeSide);
                Console.WriteLine($"Matrix of {cubeSide} side hollows: ");
                for (int i = 0; i < this.SideSize; i++)
                {
                    for (int j = 0; j < this.SideSize; j++)
                    {
                        if (matrixOfHollow[i, j].Value == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        Console.Write(matrixOfHollow[i, j]);
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            }
        }
        #endregion
    }
}
