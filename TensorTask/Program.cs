using System;
using System.Collections.Generic;

namespace TensorTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<TensorItem<int>> tensorItems = new() { };

            Tensor<int> tensor = new Tensor<int>(3, tensorItems);

            int size = 3;
            int value = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        tensor.AddItem(new TensorItem<int>(size, ++value, i, j, k));
                    }

                }
            }
            bool isNewLine = false;
            for (int i = 0; i < tensor.Length; i++)
            {
                for (int j = 0; j < tensor.Length; j++)
                {
                    for (int k = 0; k < tensor.Length; k++)
                    {
                        if (tensor[i, j, k] is not null)
                        {
                            Console.Write($"{tensor[i, j, k]}\t");
                            isNewLine = true;
                        }
                    }
                    if (isNewLine)
                    {
                        Console.WriteLine();
                        isNewLine = false;
                    }
                }
            }
        }
    }
}
