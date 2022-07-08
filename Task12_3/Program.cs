using System;
using System.Collections.Generic;
using System.IO;

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
                new BinaryOperation("^", 3, (a, b) => (Math.Pow(a, b))),
                //унарний мінус
                new UnaryOperation("=-", 4, a => -a),
                new UnaryOperation("sin", 4, a => Math.Sin(a)),
                new UnaryOperation("cos", 4, a => Math.Cos(a)),
                new UnaryOperation("sqrt", 4, a => Math.Sqrt(a)),
            };

            List<PolishNotation> polishNotationList = new();

            using (StreamReader sr = new("../../../Files/Task.txt"))
            {
                while (sr.EndOfStream == false)
                {
                    polishNotationList.Add(new PolishNotation(sr.ReadLine(), operationsList));
                }
            }

            using (StreamWriter sw = new("../../../Files/Report.txt", true))
            {
                foreach (PolishNotation polishNotation in polishNotationList)
                {
                    sw.WriteLine(PolishNotationReportFormatterService.CreateReport(polishNotation));
                }
            }

        }
    }
}
