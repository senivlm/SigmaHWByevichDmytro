using System;
using System.Collections.Generic;
using System.IO;
using Task12_3.Operation;

namespace Task12_3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Operations operationsList = new()
                {
                    new BinaryOperation("+", 1, (a, b) => (a + b)),
                    new BinaryOperation("-", 1, (a, b) => (a - b)),
                    new BinaryOperation("*", 2, (a, b) => (a * b)),
                    new BinaryOperation("/", 2, (a, b) => (a / b)),
                    new BinaryOperation("^", 3, (a, b) => (Math.Pow(a, b))),                    
                    new UnaryOperation("=-", 4, a => -a),//унарний мінус
                    new UnaryOperation("sin", 4, a => Math.Sin(a)),
                    new UnaryOperation("cos", 4, a => Math.Cos(a)),
                    new UnaryOperation("sqrt", 4, a => Math.Sqrt(a)),
                    new ConstantOperation("PI", 5, ()=> Math.PI),
                    new ConstantOperation("e", 5, ()=>Math.E)
                };

                List<PolishNotation> polishNotationList = new();
                if (ConsolePolishNotaionReaderService.TryRead(out PolishNotation polishNotationConsole, in operationsList))
                {
                    Console.WriteLine("Польский запис успішно прочитан");
                    Console.WriteLine(PolishNotationReportFormatterService.CreateReport(polishNotationConsole));
                }
                else
                {
                    Console.WriteLine("Польский запис має хибний формат, не вдалося зчитати");
                }
                
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
           

        }
    }
}
