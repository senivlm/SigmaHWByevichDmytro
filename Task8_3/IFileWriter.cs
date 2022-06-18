using System.IO;

namespace Task7
{
    internal interface IFileWriter
    {
        void WriteToStream(StreamWriter writer, bool append = false);
    }
}