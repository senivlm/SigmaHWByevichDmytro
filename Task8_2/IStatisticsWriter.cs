using System.Collections.Generic;

namespace Task8_2
{
    internal interface IStatisticsWriter
    {
        void Write(Dictionary<string,int> statistics);
    }
}