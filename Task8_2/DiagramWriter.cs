using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    internal class DiagramWriter : IStatisticsWriter
    {
        private double _koef;
        public DiagramWriter()
        {
            _koef = 1;
        }
        public void Write(Dictionary<string, int> statistics)
        {
            if (!(statistics.Values.Max() is int max))
            {
                throw new ArgumentException();
            }
            _koef = 100d / max;
            foreach (KeyValuePair<string, int> item in statistics)
            {
                int repeatSharp = (int)((int)item.Value * _koef);
                string bar = string.Concat(Enumerable.Repeat("#", repeatSharp));
                Console.WriteLine($"{string.Format("{0,16}", item.Key)}: {string.Format("|{0,-100}|", bar)} {(int)item.Value}");
            }
        }
    }
}
