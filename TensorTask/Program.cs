using System;
using System.Collections.Generic;

namespace TensorTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TensorCube<int?> cube = new TensorCube<int?>(3);

            cube.InitializeWithSameValue(1);

            cube.PrintTensorAsCube();
            for (int i = 0; i < cube.SideSize; i++)
            {
                cube[i, 0, 0].Value = null;
            }

            Console.WriteLine();

            cube.PrintTensorAsCubeSides();

            Console.WriteLine();
            Console.WriteLine(cube.IsHollowFromTo(new[] {0,0,0},Axis3D.X,cube.SideSize-1));
            
        }
    }
}
