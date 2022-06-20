namespace Task8_2
{
    internal class WebLogsHandler
    {
        private readonly WebLogs _webLogs;

        private readonly IStatisticsAnalyst _statisticsAnalyst;
        private readonly IStatisticsWriter _statisticsWriter;
        public WebLogsHandler(WebLogs webLogs, IStatisticsAnalyst statisticsAnalyst, IStatisticsWriter statisticsWriter)
        {
            _statisticsAnalyst = statisticsAnalyst;
            _statisticsWriter = statisticsWriter;
            _webLogs = new WebLogs(webLogs);
        }
        public void WriteAnalyze()
        {
            _statisticsWriter.Write(_statisticsAnalyst.Analyze(_webLogs));
        }



    }
}
