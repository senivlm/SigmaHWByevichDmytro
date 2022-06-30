using System;

namespace Task3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Vector vector = new Vector(11);
            vector.RandomInitialization(1, 10);
            Console.WriteLine(vector);
            foreach (Pair<int> item in vector.CalculateFreq())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(vector);
            Console.WriteLine(vector.IsPalindrome());
            vector.Reverse();

            Console.WriteLine(vector);
            Console.WriteLine($"frequest num :{vector.LongestSubsequence()} times ");

        }
    }
}