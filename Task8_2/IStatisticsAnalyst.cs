using System.Collections.Generic;

namespace Task8_2
{
    internal interface IStatisticsAnalyst
    {
        Dictionary<string, int> Analyze(WebLogs webLogs);
    }
}
