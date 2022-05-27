﻿using Homework4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    internal class Vector
    {
        private int[] _array;
        public int Length { get => _array.Length; }

        public int this[uint index]
        {
            get
            {
                if (index < Length)
                {
                    return _array[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                _array[index] = value;
            }

        }

        public Vector() : this(default) { }
        public Vector(int vectorLength)
        {
            _array = new int[vectorLength];
        }

        public void Bubble(Trend trend)
        {
            bool isSorted;
            switch (trend)
            {
                case Trend.decrease:
                    for (int i = 0; i < Length - 1; i++)
                    {
                        isSorted = true;
                        for (int j = 0; j < Length - 1 - i; j++)
                        {
                            if (_array[j + 1] > _array[j])
                            {
                                var tmp = _array[j];
                                _array[j] = _array[j + 1];
                                _array[j + 1] = tmp;
                                isSorted = false;
                            }
                        }
                        if (isSorted)
                            break;
                    }
                    break;
                case Trend.increase:
                    for (int i = 0; i < Length - 1; i++)
                    {
                        isSorted = true;
                        for (int j = 0; j < Length - i - 1; j++)
                        {
                            if (_array[j + 1] < _array[j])
                            {
                                var tmp = _array[j];
                                _array[j] = _array[j + 1];
                                _array[j + 1] = tmp;
                                isSorted = false;
                            }
                        }
                        if (isSorted)
                            break;
                    }
                    break;
                default:
                    throw new Exception();
            }
        }

        public void RandomInitialization(int minValue, int maxValue)
        {
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                _array[i] = random.Next(minValue, maxValue);
            }
        }
        public void ShuffleInitialization()
        {
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                int tmp;
                do
                {
                    tmp = random.Next(1, Length + 1);

                } while (_array.Contains(tmp));
                _array[i] = tmp;

            }
        }
        public void StandartReverse()
        {
            _array = _array.Reverse().ToArray();
        }
        public void Reverse()
        {
            for (int i = 0; i < Length / 2; i++)
            {
                var tmp = _array[Length - 1 - i];
                _array[Length - 1 - i] = _array[i];
                _array[i] = tmp;
            }
        }

        public IEnumerable<Pair<int>> CalculateFreq()
        {
            List<Pair<int>> pairs = new List<Pair<int>>();
            for (int i = 0; i < Length; i++)
            {
                if (pairs.Where(x => x.Element == _array[i]).Any())
                    continue;

                var tmpPair = new Pair<int>();
                tmpPair.Element = _array[i];

                for (int j = i; j < Length; j++)
                {
                    if (_array[j] == tmpPair.Element)
                        tmpPair.Freq++;
                }
                pairs.Add(tmpPair);
            }
            return pairs;
        }
        public Pair<int> LongestSubsequence()
        {
            int count;
            Pair<int> pair = new Pair<int>(_array[0], 1);
            for (int i = 0; i < Length - 1; i++)
            {
                count = 1;
                for (int j = i; j < Length - 1; j++)
                {
                    if (_array[j] == _array[j + 1])
                    {
                        count++;
                        i++;
                    }
                    else break;
                }
                if (count > pair.Freq)
                {
                    pair.Element = _array[i];
                    pair.Freq = count;
                }
            }
            return pair;
        }
        public bool IsPalindrome()
        {
            for (int i = 0; i < Length / 2; i++)
            {
                if (_array[i] != _array[Length - 1 - i])
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _array)
                sb.Append($"{item} ");

            return sb.ToString();

        }



    }
}
