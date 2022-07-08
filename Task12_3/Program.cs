using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Task12_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Operations operationsList = new()
            {
                new BinaryOperation("+", 1, (a, b) => (a + b)),
                new BinaryOperation("-", 1, (a, b) => (a - b)),
                new BinaryOperation("*", 2, (a, b) => (a * b)),
                new BinaryOperation("/", 2, (a, b) => (a / b)),
                new BinaryOperation("^", 3, (a, b) => (Math.Pow(a,b))),
                //унарний мінус
                new UnaryOperation("=-", 4, a => -a),
                new UnaryOperation("sin", 4, a => Math.Sin(a)),
                new UnaryOperation("cos", 4, a => Math.Cos(a)),
                new UnaryOperation("sqrt", 4, a => Math.Sqrt(a)),
            };
            double t = operationsList["/"].Behavior(9, 3);
            Console.WriteLine(t);

            using (StreamReader sr = new("../../../Task.txt"))
            {
                while (sr.EndOfStream == false)
                {
                    var polishNot = new PolishNotation(sr.ReadLine(), operationsList);
                    Console.WriteLine(polishNot);
                    Console.WriteLine(polishNot.Solve());
                    Console.WriteLine();
                }
            }


        }
    }
}
