namespace Task11.Validators
{
    internal delegate void LoggerOnBadFormat(string message);
    internal interface IStringParser<T>
    {
        T Parse(string str);
        event LoggerOnBadFormat OnBadFormatLogger;
    }
}
