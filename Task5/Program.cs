using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Task5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileHandler arrayFile = new FileHandler("../../../ArrayData.txt");
            Vector vector = new Vector(50);
            vector.RandomInitialization(1, 50);
            Console.WriteLine(vector);
            Stopwatch stopwatch = Stopwatch.StartNew();
            vector.HeapSort();
            stopwatch.Stop();
            Console.WriteLine(vector);
            Console.WriteLine(vector.IsSorted(Trend.increase));
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

        }
    }
}