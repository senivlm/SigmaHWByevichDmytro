using Task11.FileHandler;

namespace Task11.Validators
{
    internal delegate void LoggerOnBadFormat(string message);
    internal interface ITXTSerializedParametersParser<out T>
    {
        T Parse(TXTSerializedParameters txtSerializedParams);
        event LoggerOnBadFormat OnBadFormatLogger;
    }
}
