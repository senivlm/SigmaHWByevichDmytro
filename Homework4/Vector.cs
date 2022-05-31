using Homework4;
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
        public int Max { get => _array.Max(); }
        public int Min { get => _array.Min(); }

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
                        for (int j = 0; j < Length - i - 1; j++)
                        {
                            if (_array[j + 1] > _array[j])
                            {
                                Swap(ref _array[j], ref _array[j + 1]);
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
                                Swap(ref _array[j], ref _array[j + 1]);
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
        public void Counting()
        {
            int[] temp = new int[Max - Min + 1];
            for (int i = 0; i < Length; i++)
            {
                temp[_array[i] - Min]++;
            }
            int k = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < temp[i]; j++)
                {
                    _array[k] = i + Min;
                    k++;
                }
            }
        }

        public void QuickSort(Pivot pivotType = Pivot.middleItem, Trend trend = Trend.increase)
        {
            uint pivot = 0;
            switch (pivotType)
            {
                case Pivot.firstItem:
                    pivot = 0;
                    break;
                case Pivot.lastItem:
                    pivot = (uint)Length - 1;
                    break;
                case Pivot.middleItem:
                    pivot = (uint)(Length - 1) / 2;
                    break;
                case Pivot.avarage:
                    pivot = FindAvarage();
                    break;
                default:
                    throw new Exception();
            }
            switch (trend)
            {// краще один метод з передачею параметра та встановленню його за замовчуванням
                case Trend.decrease:
                    QuickSortRecDecrease(0, (uint)Length - 1, pivot);
                    break;
                case Trend.increase:
                    QuickSortRecIncrease(0, (uint)Length - 1, pivot);
                    break;
                default:
                    break;
            }
        }
        private void QuickSortRecIncrease(uint firstIndex, uint lastIndex, uint pivot)
        {
            if (lastIndex - firstIndex + 1 > 1)
            {
                uint i = firstIndex;
                uint j = lastIndex;
                while (i < j)
                {
                    while (_array[i] <= _array[pivot] && i < pivot)
                    {
                        i++;
                    }
                    while (_array[j] >= _array[pivot] && j > pivot)
                    {
                        j--;
                    }
                    if (i < j)
                    {
                        Swap(ref _array[i], ref _array[j]);
                        if (pivot != firstIndex && pivot != lastIndex)
                        {
                            j = lastIndex;
                            i = firstIndex;
                        }
                    }
                }
                QuickSortRecIncrease(firstIndex, j - 1, (firstIndex + j - 1) / 2);
                QuickSortRecIncrease(i + 1, lastIndex, (i + 1 + lastIndex) / 2);
            }
        }
        private void QuickSortRecDecrease(uint firstIndex, uint lastIndex, uint pivot)
        {
            if (lastIndex - firstIndex + 1 > 1)
            {
                uint i = firstIndex;
                uint j = lastIndex;
                while (i < j)
                {
                    while (_array[i] >= _array[pivot] && i < pivot)
                    {
                        i++;
                    }
                    while (_array[j] <= _array[pivot] && j > pivot)
                    {
                        j--;
                    }
                    if (i < j)
                    {
                        Swap(ref _array[i], ref _array[j]);
                        if (pivot != firstIndex && pivot != lastIndex)
                        {
                            j = lastIndex;
                            i = firstIndex;
                        }
                    }
                }
                QuickSortRecDecrease(firstIndex, j - 1, (firstIndex + j - 1) / 2);
                QuickSortRecDecrease(i + 1, lastIndex, (i + 1 + lastIndex) / 2);
            }
        }

        public bool IsSorted()
        {
            for (int i = 0; i < Length - 1; i++)
            {
                if (_array[i] > _array[i + 1])
                    return false;
            }
            return true;
        }
        private uint FindAvarage()
        {
            int sum = _array.Sum();
            var avg = sum / Length;
            int k = 1;
            while (avg + (avg / 3d) * k < Max)
            {
                for (uint i = 0; i < Length / 2; i++)
                {
                    if (_array[i] < avg + (avg / 3d) * k && _array[i] > avg - (avg / 3d) * k)
                        return i;
                    if (_array[Length - i - 1] < avg + (avg / 3d) * k && _array[Length - i - 1] > avg - (avg / 3d) * k)
                        return (uint)Length - i - 1;
                }
                k++;
            }
            return 0;
        }
        private static void Swap(ref int a, ref int b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        public void RandomInitialization(int minValue, int maxValue)
        {
            Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                _array[i] = random.Next(minValue, maxValue);
            }
        }
        public void RandomInitialization(int minValue, int maxValue, int seed)
        {
            Random random = new Random(seed);
            for (int i = 0; i < Length; i++)
            {
                _array[i] = random.Next(minValue, maxValue);
            }
        }
        public void ShuffleInitialization()
        {// не оптимізовано!
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
