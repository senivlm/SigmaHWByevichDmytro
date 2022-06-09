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
            FileHandler sortFile = new FileHandler("../../../SortedArray.txt");
            FileHandler tmpVectorFile = new FileHandler("../../../TmpVector.txt");
            try
            {
                Vector.FileSplitMergeSort(arrayFile, sortFile, tmpVectorFile, 5, Trend.decrease);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}