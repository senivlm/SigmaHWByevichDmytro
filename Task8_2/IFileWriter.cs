using System.IO;

namespace Task8_2
{
    internal interface IFileWriter
    {
        void WriteToStream(StreamWriter writer, bool append = false);
    }
}