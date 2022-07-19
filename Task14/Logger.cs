namespace Task11
{
    internal class Logger
    {
        #region Props
        private int _exCount = 0;
        private string _path;

        private static readonly Logger _instance;
        public static Logger Instance => _instance ?? new Logger();
        public int ExCount => _exCount;
        public string Path { get => _path; set => _path = value; }
        #endregion
        #region Ctors
        static Logger()
        {
            _instance = new Logger();
        }
        #endregion
        #region Methods
        public void Log(string logLine)
        {
            if (_path == null)
            {
                throw new ArgumentNullException(nameof(logLine));
            }
            if (File.Exists(_path) == false)
            {
                throw new FileNotFoundException(_path);
            }
            try
            {
                using (StreamWriter sw = new(_path, true))
                {
                    sw.WriteLine(logLine + GetLogTime());
                }
                _exCount++;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string GetLogTime()
        {
            return $"<LogTime: {DateTime.Now:G}>;";
        }
        #endregion          

    }
}
