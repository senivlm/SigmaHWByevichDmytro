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
            //FileHandler arrayFile = new FileHandler("../../../ArrayData.txt");
            //FileHandler sortedArrayFile = new FileHandler("../../../SortedArray.txt");

            //Console.WriteLine(arrayFile);
            //Vector.FileSplitMergeSort(arrayFile, sortedArrayFile, Trend.increase);
            //Console.WriteLine("Sortred: ");
            //Console.WriteLine(sortedArrayFile);


            Matrix matrix = new Matrix(5,5);
            matrix.FIll(Filling.diagonal,Direction.down);
            Console.WriteLine(matrix);



        }
    }
}