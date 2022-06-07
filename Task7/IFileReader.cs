using System.IO;

namespace Task7
{
    internal interface IFileReader
    {
        void ReadFromStream(StreamReader reader);
    }
}