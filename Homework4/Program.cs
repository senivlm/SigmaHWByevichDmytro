using Homework4;
using System;

namespace Homework3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector vector = new Vector(11);
            vector.RandomInitialization(1, 10);

            Console.WriteLine(vector); 
            vector.Bubble(Trend.increase);
            Console.WriteLine(vector);
        }
    }
}