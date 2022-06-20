using System;

namespace Task2_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Matrix matrix = new Matrix(4, 4);
            matrix.FIll(Filling.spiral, Direction.down);
            matrix.Print();

            Console.WriteLine();
            matrix.FIll(Filling.vertical, Direction.up);
            matrix.Print();

        }
    }
}
