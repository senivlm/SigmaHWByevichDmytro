using Task13.FileHandler;

namespace Task13.Readers
{
    internal interface IObjectGetterFromSerializedLine<T>
    {
        bool TryGetObject<G>(out G obj, string Line, ITXTSerializedParamsParser<G> validator) where G : T, new();
    }
}
