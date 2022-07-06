using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Task11.Readers;
using Task11.Validators;

namespace Task11.FileHandler
{
    internal static class FileHandlerService
    {

        public static bool TryReadToObject<G>(out G obj, IObjectGetterFromSerializedLine<G> streamLineReader, ITXTSerializedParametersParser<G> validator, string path)
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
                    string line = stream.ReadLine();
                    return streamLineReader.TryGetObject(out obj, line, validator);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ReadToCollection<T, G>(ref T obj, IStreamCollectionReader<T, G> collectionReader, Dictionary<string, ITXTSerializedParametersParser<G>> parser, string path)
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
        public static void WriteToFile<T,G>(T obj, ISerializer<G> serializer, string path, bool append = false)
            where T : class
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
                    writer.WriteLine(serializer.Serialize(obj));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void WriteToFileCollection<T, G>(T obj, ISerializer<G> serializer, string path, bool append = false)
            where T : IEnumerable
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
                    foreach (object item in obj)
                    {
                        writer.WriteLine(serializer.Serialize(item));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
