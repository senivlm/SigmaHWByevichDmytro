using System;

namespace Task12_3
{
    internal static class ConsolePolishNotaionReaderService
    {
        public static bool TryRead(out PolishNotation polishNotation,in Operations operations)
        {
            polishNotation = new();
            Console.WriteLine("Введіть вираз складений з таких дій:");
            foreach (string item in operations.Keys)
            {
                Console.WriteLine(item);
            }
            Console.Write("Введіть > ");
            string line = Console.ReadLine();
            try
            {
                polishNotation = new(line, operations);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
