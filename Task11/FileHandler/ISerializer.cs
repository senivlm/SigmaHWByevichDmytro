using Task11.FileHandler;

namespace Task11
{
    /// <summary>
    /// Серіалізує дані типу G у тип T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface ISerializer<T>
    {
        T Serialize<G>(in G obj) where G : class;
    }
}