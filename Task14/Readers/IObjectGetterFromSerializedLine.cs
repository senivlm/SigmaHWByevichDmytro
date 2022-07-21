using Task14.Validators;

namespace Task14.Readers
{
    internal interface IObjectGetterFromSerializedLine<T>
    {
        bool TryGetObject<G>(out G obj, string Line, ITXTSerializedParametersParser<G> validator) where G : T, new();
    }
}
