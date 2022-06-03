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
            //arrayFile.AddToFile("32");
            Random random = new Random();
            //Vector vector = new Vector(arrayFile.GetIntCollectionFromFile());
            Vector vector = new Vector(1000);
            vector.RandomInitialization(-10000, 10000);

            Console.WriteLine(vector);
            Stopwatch stopwatch = Stopwatch.StartNew();
            try
            {
                vector.HeapSort(Trend.increase);
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            stopwatch.Stop();
            Console.WriteLine();
            Console.WriteLine(vector);
            Console.WriteLine(vector.IsSorted(Trend.increase));
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");


        }
    }
}