using System;
using System.Collections.Generic;
using System.IO;
using Task11.Readers;
using Task11.Validators;

namespace Task11.FileHandler
{
    internal static class FileHandlerService
    {

        public static bool TryReadToObject<G>(out G obj, IStreamLineReader<G> streamReader, IStringParser<G> validator, string path)
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
                    return streamReader.TryReadLine(out obj, stream, validator);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ReadToCollection<T, G>(ref T obj, IStreamCollectionReader<T, G> collectionReader, Dictionary<string, IStringParser<G>> parser, string path)
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
                    collectionReader.ReadCollection(ref obj, stream, parser);
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
