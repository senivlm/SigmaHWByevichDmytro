using System;

namespace Task8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VisitLogModel visitLog = new VisitLogModel("90.12.140.0   ", DateTime.Now);
                Console.WriteLine(visitLog);

                WebLogs webLogs = new WebLogs(new[] { visitLog } );
                Console.WriteLine(webLogs);

                visitLog.IP = "90.12.120.012";
                Console.WriteLine(webLogs);
                Console.WriteLine(visitLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
