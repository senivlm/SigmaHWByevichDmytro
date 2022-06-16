using System;

namespace Task8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VisitLogsGenerator generator = new VisitLogsGenerator(10,10);
                generator.GenerateLogs();
                Console.WriteLine(generator);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
