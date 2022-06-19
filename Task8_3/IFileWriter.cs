using System.IO;

namespace Task8_3
{
    internal interface IFileWriter
    {
        void WriteToStream(StreamWriter writer, bool append = false);
    }
}