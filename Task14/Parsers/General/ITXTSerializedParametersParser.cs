using Task14.FileHandler;

namespace Task14.Validators
{
    internal delegate void LoggerOnBadFormat(string message);
    internal interface ITXTSerializedParametersParser<out T>
    {
        T Parse(TXTSerializedParameters txtSerializedParams);
        event LoggerOnBadFormat OnBadFormatLogger;
    }
}
