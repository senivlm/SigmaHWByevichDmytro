using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    internal class VisitLogsGenerator: IFileWriter
    {
        private WebLogs _generatedLogs;
        private int _logsAmount;
        private int _ipAmount;
        private string[] _ipArray;

        Random rnd = new Random();
        public int LogsAmount
        {
            get => _logsAmount;
            set => _logsAmount = value;
        }

        public VisitLogsGenerator() : this(default,default) { }
        public VisitLogsGenerator(int logsAmount, int ipAmount)
        {
            _generatedLogs = new WebLogs();
            _logsAmount = logsAmount;
            _ipAmount = ipAmount;
            _ipArray = new string[ipAmount];
            GenerateIPArray();
        }
        public void GenerateIPArray()
        {
            for (int i = 0; i < _ipArray.Length; i++)
            {
                _ipArray[i] = GenerateIP();
            }
        }
        public void GenerateLogs()
        {
            for (int i = 0; i < LogsAmount; i++)
            {
                _generatedLogs.Add(GenerateRandomLog());
            }
        }
        private string GenerateIP()
        {
            return $"{rnd.Next(250)}.{rnd.Next(250)}.{rnd.Next(250)}.{rnd.Next(250)}";
        }
        private VisitLogModel GenerateRandomLog()
        {
            int ipIndex = rnd.Next(_ipArray.Length);

            DayOfWeek logDayOfWeek = (DayOfWeek)rnd.Next(0,7);

            TimeSpan logTime = new TimeSpan(rnd.Next(24), rnd.Next(60), rnd.Next(60));

            return new VisitLogModel(_ipArray[ipIndex], logDayOfWeek, logTime);
        }
        public void WriteToStream(StreamWriter writer, bool append = false)
        {
            writer.Write(this.ToString());
        }

        public override string ToString()
        {
            return _generatedLogs.ToString();
        }

    }
}
