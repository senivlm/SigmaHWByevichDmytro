using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Task5
{
    internal class Matrix : IEnumerable
    {
        private int[,] _matrix;
        private int _columns;
        private int _rows;
        private Filling _filling;
        private Direction _direction;
        public int Max
        {
            get
            {
                int max = _matrix[0, 0];
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        if (max < _matrix[i, j])
                        {
                            max = _matrix[i, j];
                        }
                    }
                }
                return max;
            }
        }
        public Matrix() : this(default, default) { }
        public Matrix(int columns, int rows)
        {

            _columns = columns;
            _rows = rows;
            _matrix = new int[_rows, _columns];

        }
        public Matrix(StreamReader reader)
        {
            ReadMatrixFromFile(reader);
        }
        public void FIll(Filling filling, Direction direction)
        {
            _filling = filling;
            _direction = direction;
            switch (_filling)
            {
                case Filling.vertical:
                    FillVertical();
                    break;
                case Filling.diagonal:
                    FillDiagonal();
                    break;
                case Filling.spiral:
                    FillSpiral();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        private bool SetDirection()
        {
            switch (_direction)
            {
                case Direction.up:
                    return true;
                case Direction.down:
                    return false;
                default:
                    throw new ArgumentException();
            }
        }
        private void FillVertical()
        {

            int value = 1;
            bool isDirectionUp = SetDirection();

            for (int column = 0; column < _columns; column++)
            {
                if (isDirectionUp)
                {
                    for (int row = _rows - 1; row >= 0; row--)
                    {
                        _matrix[row, column] = value++;
                    }
                    isDirectionUp = false;
                    continue;
                }

                for (int row = 0; row < _rows; row++)
                {
                    _matrix[row, column] = value++;
                }
                isDirectionUp = true;

            }
        }
        private void FillDiagonal()
        {
            bool isDirectionUp = SetDirection();
            int value = 0;

            for (int diag = 0; diag < _rows * _columns; diag++)
            {
                if (isDirectionUp)
                {
                    for (int row = 0; row < _rows; row++)
                    {
                        for (int col = 0; col < _columns; col++)
                        {
                            if (row + col == diag)
                            {
                                _matrix[row, col] = ++value;
                            }
                        }
                    }
                    isDirectionUp = isDirectionUp ? false : true;
                }
                else
                {
                    for (int col = 0; col < _columns; col++)
                    {
                        for (int row = 0; row < _rows; row++)
                        {
                            if (row + col == diag)
                            {
                                _matrix[row, col] = ++value;
                            }
                        }
                    }
                    isDirectionUp = isDirectionUp ? false : true;
                }
            }
        }
        private void FillSpiral()
        {
            int column = 0;
            int row = 0;
            int rowStart = 0;
            int rowEnd = 0;
            int columnStart = 0;
            int columnEnd = 0;
            bool isDirectionUp = SetDirection();
            if (!isDirectionUp)
            {
                for (int i = 0; i < _matrix.Length;)
                {
                    _matrix[row, column] = ++i;
                    if (column == columnStart && row < _rows - rowEnd - 1)
                    {
                        row++;
                    }
                    else if (row == _rows - rowEnd - 1 && column != _columns - columnEnd - 1)
                    {
                        column++;
                    }
                    else if (column == _columns - columnEnd - 1 && row > rowStart)
                    {
                        row--;
                    }
                    else
                    {
                        column--;
                    }
                    if (column == columnStart + 1 && row == rowStart && rowStart != _rows - rowEnd - 1)
                    {
                        columnStart++;
                        rowStart++;
                        columnEnd++;
                        rowEnd++;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _matrix.Length;)
                {
                    _matrix[row, column] = ++i;
                    if (row == rowStart && column < _columns - columnEnd - 1)
                    {
                        column++;
                    }
                    else if (column == _columns - columnEnd - 1 && row != _rows - rowEnd - 1)
                    {
                        row++;
                    }
                    else if (row == _rows - rowEnd - 1 && column > columnStart)
                    {
                        column--;
                    }
                    else
                    {
                        row--;
                    }
                    if (row == rowStart + 1 && column == columnStart && columnStart != _columns - columnEnd - 1)
                    {
                        columnStart++;
                        rowStart++;
                        columnEnd++;
                        rowEnd++;
                    }
                }
            }
        }
        public void ReadMatrixFromFile(StreamReader reader)
        {
            string line = reader.ReadLine();
            string[] sizes = line.Split(' ');

            this._rows = int.Parse(sizes[0]);
            this._columns = int.Parse(sizes[1]);

            _matrix = new int[_rows, _columns];
            for (int i = 0; i < _rows; i++)
            {
                string[] items = reader.ReadLine().Split(' ');
                for (int j = 0; j < _columns; j++)
                {
                    _matrix[i, j] = int.Parse(items[j]);
                }
            }
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _columns; i++)
            {
                for (int j = 0; j < _rows; j++)
                {
                    yield return _matrix[j, i];
                }
            }
        }

        public void Print()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    Console.Write($"{_matrix[row, column]} ");
                }
                Console.WriteLine();
            }
        }

        public override string ToString()
        {
            string formatSpace = "{0," + (Max.ToString().Length + 1) + "}";
            StringBuilder stringBuilder = new StringBuilder();
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    stringBuilder.Append(string.Format(formatSpace, $"{_matrix[row, column]}"));
                }
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();


        }


    }
}