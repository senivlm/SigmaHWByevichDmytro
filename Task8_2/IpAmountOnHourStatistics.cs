using System.Collections.Generic;

namespace Task8_2
{
    internal class IpAmountOnHourStatistics : IStatisticsAnalyst
    {
        public Dictionary<string, int> Analyze(WebLogs webLogs)
        {
            SortedDictionary<string, int> statistics = new SortedDictionary<string, int>();

            for (int i = 0; i < webLogs.Length; i++)
            {
                string key = $"{webLogs[i].Logtime.Hours}-{webLogs[i].Logtime.Hours + 1}";
                if (statistics.ContainsKey(key))
                {
                    statistics[key]++;
                }
                else
                {
                    statistics.Add(key, 1);
                }
            }

            return new Dictionary<string, int>(statistics);
        }
    }
}
