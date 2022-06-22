namespace Task9.Validator
{
    internal interface IStringValidator<T>
    {
        bool IsValid(string str);
        void Validate(string str, T obj);
    }
}
