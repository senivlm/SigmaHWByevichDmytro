using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    internal class VisitsPerIpStatistics : IStatisticsAnalyst
    {
        public Dictionary<string, int> Analyze(WebLogs webLogs)
        {
            Dictionary<string, int> statistics = new Dictionary<string, int>();
            for (int i = 0; i < webLogs.Length; i++)
            {
                if (statistics.ContainsKey(webLogs[i].IP.ToString()))
                {
                    statistics[webLogs[i].IP.ToString()]++;
                }
                else
                {
                    statistics.Add(webLogs[i].IP.ToString(), 1);
                }
            }

            return statistics;
        }
    }
}
