using System;
using System.Collections.Generic;
using System.IO;

namespace Lesson02_05
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<(string, int, double)> students = new List<(string, int, double)>();
            try
            {
                using (StreamReader reader = new StreamReader("../../../Data.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            students.Add(Parse(reader.ReadLine()));
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Item1}, Birth year: {student.Item2}, Avarage bal: {student.Item3}");
            }

        }
        public static (string, int, double) Parse(string str)
        {
            string[] data = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string exMessage = null;
            if (data.Length != 3)
            {
                throw new FormatException($"Invalid info about student format: {str} ");
            }
            string surname = data[0];

            if (!int.TryParse(data[1], out int year))
            {
                exMessage += "Invalid year format; ";
            }
            if (!double.TryParse(data[2], out double avarage))
            {
                exMessage += "Invalid avarage format; ";
            }
            if (exMessage is not null)
            {
                throw new FormatException(exMessage);
            }
            return (surname, year, avarage);
        }
    }
}
