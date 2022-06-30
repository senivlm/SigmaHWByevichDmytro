using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Readers;
using Task11.Validators;

namespace Task11.FileHandler
{
    internal static class FileHandlerService
    {

        public static void ReadToObject<G>(out G obj, IStreamLineReader<G> streamReader, IStringParser<G>? validator, string path)
            where G : new()
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    streamReader.ReadLine(out obj, stream, validator);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ReadToCollection<T, G>(out T obj, IStreamCollectionReader<T, G> collectionReader, Dictionary<string, IStringParser<G>> validator, string path)
            where T : IEnumerable<G>
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            try
            {
                using (StreamReader stream = new StreamReader(path))
                {
                    collectionReader.ReadCollection(out obj, stream, validator);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void WriteToFile(ITXTSerializer obj, string path, bool append = false)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(path, append))
                {
                    if (append)
                    {
                        writer.WriteLine();
                    }
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
