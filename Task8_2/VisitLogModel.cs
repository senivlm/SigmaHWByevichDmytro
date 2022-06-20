using System;

namespace Task8_2
{
    internal class VisitLogModel
    {
        private string _ip;
        private DayOfWeek _logDayOfWeek;
        private TimeSpan _logtime;
        public string IP
        {
            get => _ip;
            set
            {
                if (!TryValidateIP(value, out _ip))
                {
                    throw new ArgumentException($"Хибний формат IP: {value}");
                }
            }
        }

        public DayOfWeek LogDayOfWeek
        {
            get => _logDayOfWeek;
            set => _logDayOfWeek = value;
        }
        public TimeSpan Logtime { get => _logtime; set => _logtime = value; }

        public VisitLogModel() : this(string.Empty, default, default) { }
        public VisitLogModel(string ip, DayOfWeek logDayOfWeek, TimeSpan logtime)
        {
            IP = ip;
            _logDayOfWeek = logDayOfWeek;
            _logtime = logtime;
        }
        public VisitLogModel(VisitLogModel other)
        {
            IP = other.IP;
            _logDayOfWeek = other._logDayOfWeek;
            _logtime = other._logtime;
        }
        static public bool TryParse(string value, out VisitLogModel result)
        {
            result = default;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            string[] parts = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                return false;
            }
            if (!TryValidateIP(parts[0], out string ipResult))
            {
                return false;
            }
            if (!Enum.TryParse<DayOfWeek>(parts[1], out DayOfWeek dayOfWeekResult))
            {
                return false;
            }
            if (!TimeSpan.TryParse(parts[2], out TimeSpan timeSpanResult))
            {
                return false;
            }
            result = new VisitLogModel(ipResult, dayOfWeekResult, timeSpanResult);
            return true;

        }

        private static bool TryValidateIP(string ip, out string result)
        {
            result = string.Empty;
            ip = ip.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }

            string[] splitedIp = ip.Split('.');

            if (splitedIp.Length != 4)
            {
                return false;
            }

            foreach (string item in splitedIp)
            {
                if (item.Length != item.Trim().Length)
                {
                    return false;
                }
                try
                {
                    byte.Parse(item);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            result = ip;
            return true;
        }

        public override string ToString()
        {
            return $"{IP} {_logDayOfWeek} {_logtime}";
        }
    }
}
