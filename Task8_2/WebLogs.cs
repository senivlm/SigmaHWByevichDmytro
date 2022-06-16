using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    internal class WebLogs : IFileReader, IFileWriter
    {
        private List<VisitLogModel> _visitLogs;
        public int Length => _visitLogs.Count;
        public VisitLogModel this[int index]
        {
            get
            {
                if (!(index >= 0 && index < _visitLogs.Count))
                {
                    throw new IndexOutOfRangeException();
                }
                return _visitLogs[index];
            }
        }
        public WebLogs()
        {
            _visitLogs = new();

        }
        public WebLogs(WebLogs webLogs) : this()
        {
            for (int i = 0; i < webLogs.Length; i++)
            {
                _visitLogs.Add(new VisitLogModel(webLogs[i]));
            }
        }

        public WebLogs(IEnumerable<VisitLogModel> visitLogs) : this()
        {
            if (visitLogs is null)
            {
                throw new ArgumentNullException();
            }
            foreach (VisitLogModel log in visitLogs)
            {
                _visitLogs.Add(new(log));
            }
        }
        public void Add(VisitLogModel visitLog)
        {
            _visitLogs.Add(new(visitLog));
        }
        public void ReadFromStream(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (!VisitLogModel.TryParse(line, out VisitLogModel visitLog))
                {
                    throw new ArgumentException($"Хибний формат запису: {line}");
                }
                this.Add(visitLog);
            }
        }
        public void WriteToStream(StreamWriter writer, bool append = false)
        {
            writer.Write(this.ToString());
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (VisitLogModel visitLog in _visitLogs)
            {
                sb.AppendLine(visitLog.ToString());
            }
            return sb.ToString();
        }

       
    }
}
