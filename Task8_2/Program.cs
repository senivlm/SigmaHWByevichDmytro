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

                VisitLogsGenerator generator = new VisitLogsGenerator(5000, 50);
                generator.GenerateLogs();
                siteStatisticsFile.WriteFromObject(generator);

                WebLogs webLogs = new WebLogs();
                siteStatisticsFile.ReadToObject(webLogs);
                    
                DiagramWriter diagramWriter = new DiagramWriter();

                Console.WriteLine("Visit amount per day of week");
                WebLogsHandler webLogsHandler = new WebLogsHandler(webLogs, new IpAmountOnDayOfWeekStatistics(), diagramWriter);
                webLogsHandler.WriteAnalyze();

                Console.WriteLine();
                Console.WriteLine("Visit amount per hour");
                webLogsHandler = new WebLogsHandler(webLogs, new IpAmountOnHourStatistics(), diagramWriter);
                webLogsHandler.WriteAnalyze();

                Console.WriteLine();
                Console.WriteLine("Visit amount per IP");
                webLogsHandler = new WebLogsHandler(webLogs, new VisitsPerIpStatistics(), diagramWriter);
                webLogsHandler.WriteAnalyze();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
