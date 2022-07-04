namespace Task11.Validators
{
    delegate void LoggerOnBadFormat(string message);
    internal interface IStringParser<T>
    {
        T Parse(string str);
        event LoggerOnBadFormat OnBadFormatLogger;
    }
}
