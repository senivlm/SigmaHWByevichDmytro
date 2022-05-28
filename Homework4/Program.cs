using Homework4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Homework3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector vectorAvaragePivot = new Vector(100000);
            Vector vectorMiddlePivot = new Vector(100000);
            var resultTimeAvaragePivot = new List<int>();
            var resultTimeMiddlePivot = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                vectorAvaragePivot.RandomInitialization(1, 5000, i);
                vectorMiddlePivot.RandomInitialization(1, 5000, i);

                var st = Stopwatch.StartNew();
                vectorAvaragePivot.QuickSort(Pivot.avarage);
                st.Stop();
                resultTimeAvaragePivot.Add((int)st.ElapsedMilliseconds);

                Console.WriteLine("pivot is avarage");
                Console.WriteLine("Is sorted: " + vectorAvaragePivot.IsSorted());
                Console.WriteLine("time: " + st.ElapsedMilliseconds);
                Console.WriteLine();

                var st1 = Stopwatch.StartNew();
                vectorMiddlePivot.QuickSort(Pivot.middleItem);
                st1.Stop();
                resultTimeMiddlePivot.Add((int)st1.ElapsedMilliseconds);

                Console.WriteLine("pivot is middle");
                Console.WriteLine("Is sorted: " + vectorMiddlePivot.IsSorted());
                Console.WriteLine("time: " + st1.ElapsedMilliseconds);
                Console.WriteLine();
            }

            Console.WriteLine("\nAvaragePivotTime - MiddlePivotTime");
            for (int i = 0; i < resultTimeAvaragePivot.Count; i++)
            {
                Console.WriteLine($"{resultTimeAvaragePivot[i]} - {resultTimeMiddlePivot[i]}");
            }
            Console.WriteLine($"avarage pivot: avaragetime: { resultTimeAvaragePivot.Sum() / resultTimeAvaragePivot.Count}, min: {resultTimeAvaragePivot.Min()}, max: {resultTimeAvaragePivot.Max()}");
            Console.WriteLine($"middle pivot: avarage time: { resultTimeMiddlePivot.Sum() / resultTimeMiddlePivot.Count}, min: {resultTimeMiddlePivot.Min()}, max: {resultTimeMiddlePivot.Max()}");
        }
    }
}