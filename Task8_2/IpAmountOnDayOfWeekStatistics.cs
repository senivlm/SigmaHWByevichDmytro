using System.Collections.Generic;

namespace Task8_2
{
    internal class IpAmountOnDayOfWeekStatistics : IStatisticsAnalyst
    {
        public Dictionary<string, int> Analyze(WebLogs webLogs)
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            for (int i = 0; i < webLogs.Length; i++)
            {
                if (statistics.ContainsKey(webLogs[i].LogDayOfWeek.ToString()))
                {
                    statistics[webLogs[i].LogDayOfWeek.ToString()]++;
                }
                else
                {
                    statistics.Add(webLogs[i].LogDayOfWeek.ToString(), 1);
                }
            }

            return statistics;
        }
    }
}
