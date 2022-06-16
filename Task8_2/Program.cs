using System;

namespace Task8_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileHandler siteStatisticsFile = new FileHandler("../../../SiteStatistics.txt");
                //VisitLogsGenerator generator = new VisitLogsGenerator(100,100);
                //generator.GenerateLogs();
                //siteStatisticsFile.WriteFromObject(generator);
                WebLogs webLogs = new WebLogs();
                siteStatisticsFile.ReadToObject(webLogs);

                Console.WriteLine(webLogs);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
