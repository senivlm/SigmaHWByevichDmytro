using System;
using System.IO;
using Task9.Readers;
using Task9.Validator;

namespace Task9.FIleHandler
{
    internal class FileHandler<T>
    {
        public string Path { get; set; }

        public FileHandler(string path)
        {
            Path = path;
        }
        public void ReadToObject(out T obj, IStreamReader<T> streamReader, IStringValidator<T>? validator)
        {

            if (!File.Exists(Path))
            {
                throw new FileNotFoundException();
            }
            try
            {
                using (StreamReader stream = new StreamReader(Path))
                {
                    streamReader.Read(out obj, stream, validator);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void WriteToFile(ITxtSerializer obj, bool append = false)
        {
            if (!File.Exists(Path))
            {
                throw new FileNotFoundException();
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(Path, append))
                {
                    writer.WriteLine(obj.SerializeTxt());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
