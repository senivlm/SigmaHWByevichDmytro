namespace Task11.Validators
{
    internal interface IStringParser<T>
    {
        T Parse(string str);
    }
}
