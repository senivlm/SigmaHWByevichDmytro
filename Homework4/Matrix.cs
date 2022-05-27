using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    internal class Matrix
    {
        private int[,] _matrix;
        private int _columns;
        private int _rows;
        private Filling _filling;
        private Direction _direction;
        public Matrix() : this(default, default) { }
        public Matrix(int columns, int rows)
        {

            _columns = columns;
            _rows = rows;
            _matrix = new int[_rows, _columns];

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
            bool isDiagonal = false;

            int column = 0;
            int row = 0;

            for (int value = 0; value < _matrix.Length;)
            {
                if (row == _rows - 1 || column == _columns - 1)
                    isDiagonal = true;

                _matrix[row, column] = (++value);

                if (value == _matrix.Length)
                    break;

                if (!isDiagonal)
                {
                    if (isDirectionUp)
                    {
                        column++;
                        while (column != 0)
                        {
                            _matrix[row, column] = (++value);
                            row++;
                            column--;
                        }
                        isDirectionUp = false;
                        continue;
                    }
                    row++;
                    while (row != 0)
                    {
                        _matrix[row, column] = (++value);
                        row--;
                        column++;
                    }
                    isDirectionUp = true;
                    continue;
                }
                if (!isDirectionUp)
                {
                    column++;
                    while (column != _columns - 1)
                    {
                        _matrix[row, column] = (++value);
                        row--;
                        column++;
                    }
                    isDirectionUp = true;
                    continue;
                }
                row++;
                while (row != _rows - 1)
                {
                    _matrix[row, column] = (++value);
                    row++;
                    column--;
                }
                isDirectionUp = false;

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
            if (isDirectionUp)
                for (int i = 0; i < _matrix.Length;)
                {
                    _matrix[row, column] = ++i;
                    if (column == columnStart && row < _rows - rowEnd - 1)
                        row++;
                    else if (row == _rows - rowEnd - 1 && column != _columns - columnEnd - 1)
                        column++;
                    else if (column == _columns - columnEnd - 1 && row > rowStart)
                        row--;
                    else
                        column--;
                    if (column == columnStart + 1 && row == rowStart && rowStart != _rows - rowEnd - 1)
                    {
                        columnStart++;
                        rowStart++;
                        columnEnd++;
                        rowEnd++;
                    }
                }
            else
                for (int i = 0; i < _matrix.Length;)
                {
                    _matrix[row, column] = ++i;
                    if (row == rowStart && column < _columns - columnEnd - 1)
                        column++;
                    else if (column == _columns - columnEnd - 1 && row != _rows - rowEnd - 1)
                        row++;
                    else if (row == _rows - rowEnd - 1 && column > columnStart)
                        column--;
                    else
                        row--;
                    if (row == rowStart + 1 && column == columnStart && columnStart != _columns - columnEnd - 1)
                    {
                        columnStart++;
                        rowStart++;
                        columnEnd++;
                        rowEnd++;
                    }
                }
        }


        public void Print()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    Console.Write($"{_matrix[row, column]}, ");
                }
                Console.WriteLine();
            }
        }

    }
}