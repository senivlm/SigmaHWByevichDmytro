using Task11.Validators;

namespace Task11.Readers
{
    internal interface IObjectGetterFromSerializedLine<T>
    {
        bool TryGetObject<G>(out G obj, string Line, ITXTSerializedParametersParser<G> validator) where G : T, new();
    }
}
