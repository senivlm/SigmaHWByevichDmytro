using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    internal class Vector
    {
        #region Props
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
        #endregion
        #region Ctors

        public Vector() : this(0) { }
        public Vector(int vectorLength)
        {
            _array = new int[vectorLength];
        }
        public Vector(IEnumerable<int> array)
        {
            int[] arr = array.ToArray();
            _array = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                _array[i] = arr[i];
        }
        #endregion
        #region Sorting
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
            QuickSortRecur(0, (uint)Length - 1, pivot, trend);
        }
        private void QuickSortRecur(uint firstIndex, uint lastIndex, uint pivot, Trend trend)
        {
            if (lastIndex - firstIndex + 1 > 1)
            {
                uint i = firstIndex;
                uint j = lastIndex;
                while (i < j)
                {
                    if (trend is Trend.decrease)
                    {
                        while (_array[i] >= _array[pivot] && i < pivot)
                        {
                            i++;
                        }
                        while (_array[j] <= _array[pivot] && j > pivot)
                        {
                            j--;
                        }
                    }
                    else
                    {
                        while (_array[i] <= _array[pivot] && i < pivot)
                        {
                            i++;
                        }
                        while (_array[j] >= _array[pivot] && j > pivot)
                        {
                            j--;
                        }
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
                QuickSortRecur(firstIndex, j - 1, (firstIndex + j - 1) / 2, trend);
                QuickSortRecur(i + 1, lastIndex, (i + 1 + lastIndex) / 2, trend);
            }
        }
        private void Merge(int firstIndex, int middle, int lastIndex, Trend trend)
        {
            int i = firstIndex;
            int j = middle;
            int k = 0;
            int[] temp = new int[lastIndex - firstIndex + 1];
            while (i < middle && j < lastIndex)
            {
                if (trend is Trend.increase)
                {

                    if (_array[i] < _array[j])
                    {
                        temp[k] = _array[i++];
                    }
                    else
                    {
                        temp[k] = _array[j++];
                    }
                }
                else
                {
                    if (_array[i] > _array[j])
                    {
                        temp[k] = _array[i++];
                    }
                    else
                    {
                        temp[k] = _array[j++];
                    }
                }
                k++;
            }
            if (i == middle)
            {
                for (int t = j; t < lastIndex; t++)
                {
                    temp[k++] = _array[t];
                }
            }
            else
            {
                while (i < middle)
                {
                    temp[k++] = _array[i++];
                }
            }
            for (int n = 0; n < temp.Length - 1; n++)
            {
                _array[n + firstIndex] = temp[n];
            }

        }
        private void SplitMergeSortRecur(int firstIndex, int lastIndex, Trend trend)
        {
            if (lastIndex - firstIndex <= 1) return;

            int middle = (firstIndex + lastIndex) / 2;
            SplitMergeSortRecur(firstIndex, middle, trend);
            SplitMergeSortRecur(middle, lastIndex, trend);
            Merge(firstIndex, middle, lastIndex, trend);
        }
        public void SplitMergeSort(Trend trend = Trend.increase)
        {
            SplitMergeSortRecur(0, Length, trend);
        }
        public bool IsSorted(Trend trend = Trend.increase)
        {
            if (trend is Trend.increase)
            {
                for (int i = 0; i < Length - 1; i++)
                {
                    if (_array[i] > _array[i + 1])
                    {
                        Console.WriteLine($"{i}: {_array[i]}; {i + 1}: {_array[i + 1]}");
                        return false;
                    }
                }

            }
            else
            {
                for (int i = 0; i < Length - 1; i++)
                {
                    if (_array[i] < _array[i + 1])
                    {
                        Console.WriteLine($"{i}: {_array[i]}; {i + 1}: {_array[i + 1]}");
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Метод сортує дані з файлу, використовуючі стек одночасно тільки для однієї частини масиву, проміжні результати, початкові дані та результат зберігаються у файлах
        /// </summary>
        /// <param name="dataFile">файл з невідсортованим масивом</param>
        /// <param name="sortedDataFile">файл реузльтату сортування</param>
        /// <param name="tmpVectorFile">файл для проміжних результатів сортування</param>
        /// <param name="partsAmount">кулькість частин для поділу початкового масиву</param>
        /// <param name="trend">направлення сортування</param>
        static public void FileSplitMergeSort(FileHandler dataFile, FileHandler sortedDataFile, FileHandler tmpVectorFile, uint partsAmount, Trend trend)
        {
            sortedDataFile.Clear();
            tmpVectorFile.Clear();
            using (Stream reader = File.OpenRead(dataFile.Path))
            {
                SplitReader splitReader = new SplitReader(reader, partsAmount);
                do
                {
                    Vector currentPartVector = new Vector(splitReader.GetNextIntPart());
                    currentPartVector.SplitMergeSort(trend);
                    if (splitReader.CurrentPart % 2 != 0)
                    {
                        tmpVectorFile.MergeWriteToFile(currentPartVector, sortedDataFile.Path, trend);
                        if (splitReader.IsLastPart())
                        {
                            sortedDataFile.Copy(tmpVectorFile);
                            break;
                        }
                    }
                    else
                    {
                        sortedDataFile.MergeWriteToFile(currentPartVector, tmpVectorFile.Path, trend);
                        if (splitReader.IsLastPart())
                        {
                            break;
                        }
                    }
                } while (!splitReader.IsLastPart());
            }
            tmpVectorFile.Clear();
        }

        public void HeapSort(Trend trend)
        {
            for (uint i = 0; i < _array.Length; i++)
            {
                Heapify(0, (uint)_array.Length - i - 1, trend);
                Swap(ref _array[0], ref _array[Length - 1 - i]);
            }
        }
        public void Heapify(uint currentIndex, uint lastIndex, Trend trend)
        {
            uint leftChildIndex = currentIndex == 0 ? 1 : 2 * currentIndex;
            uint rightChildIndex = currentIndex == 0 ? 2 : 2 * currentIndex + 1;
            uint parrentIndex = currentIndex == 0 ? 0 : currentIndex / 2;
            if (leftChildIndex > lastIndex)
            {
                return;
            }
            if (trend is Trend.increase)
            {
                uint maxChildIndex = leftChildIndex;
                if (rightChildIndex <= lastIndex)
                {
                    maxChildIndex = _array[leftChildIndex] > _array[rightChildIndex] ? leftChildIndex : rightChildIndex;
                }
                if (_array[currentIndex] < _array[maxChildIndex])
                {
                    Swap(ref _array[currentIndex], ref _array[maxChildIndex]);
                    Heapify(parrentIndex, lastIndex, trend);
                }
                Heapify(maxChildIndex, lastIndex, trend);
                Heapify(leftChildIndex == maxChildIndex ? rightChildIndex : leftChildIndex, lastIndex, trend);
            }
            else
            {
                uint minChildIndex = leftChildIndex;
                if (rightChildIndex <= lastIndex)
                {
                    minChildIndex = _array[leftChildIndex] < _array[rightChildIndex] ? leftChildIndex : rightChildIndex;
                }
                if (_array[currentIndex] > _array[minChildIndex])
                {
                    Swap(ref _array[currentIndex], ref _array[minChildIndex]);
                    Heapify(parrentIndex, lastIndex, trend);
                }
                Heapify(minChildIndex, lastIndex, trend);
                Heapify(leftChildIndex == minChildIndex ? rightChildIndex : leftChildIndex, lastIndex, trend);
            }
        }


        #endregion

        public static Vector operator +(Vector a, Vector b)
        {
            if (b.Length > a.Length)
            {
                (a,b) = (b,a);
            }
            
            Vector res = new Vector(a._array);

            for (int i = 0; i < b.Length; i++)
            {
                res._array[i] += b._array[i];
            }

            return res;
        }

        #region Utilities
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
        #endregion
        #region Initialization
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
        #endregion
        #region ObjectOverrides
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _array)
                sb.Append($"{item} ");
            return sb.ToString();
        }
        #endregion
    }
}
