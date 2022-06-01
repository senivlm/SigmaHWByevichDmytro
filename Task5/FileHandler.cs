using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    internal class FileHandler
    {
        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public FileHandler() : this(null) { }
        public FileHandler(string path)
        {
            Path = path;
        }
        public string ReadLine()
        {
            string line = null;
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    line = reader.ReadLine();
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл не знайдено");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return line;
        }
        public void WriteToFile(string data)
        {
            try
            {
                using (StreamWriter reader = new StreamWriter(Path))
                {
                    reader.Write(data);
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл не знайдено");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IEnumerable<int> GetIntCollectionFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    try
                    {
                        return reader.ReadToEnd().Trim().Split(' ').Select(x => int.Parse(x));
                    }
                    catch (ArgumentException)
                    {
                        throw new ArgumentException("Невірний формат запису у файлі");
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                };
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл не знайдено");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void MergeWriteToFile(Vector firsVector, Vector secondVector, Trend trend)
        {
            uint i = 0;
            uint j = 0;
            try
            {
                using (StreamWriter reader = new StreamWriter(Path))
                {
                    while (i < firsVector.Length && j < secondVector.Length)
                    {
                        if (trend is Trend.increase)
                        {
                            if (firsVector[i] < secondVector[j])
                            {
                                reader.Write(firsVector[i++] + " ");
                            }
                            else
                            {
                                reader.Write(secondVector[j++] + " ");
                            }
                        }
                        else
                        {
                            if (firsVector[i] > secondVector[j])
                            {
                                reader.Write(firsVector[i++] + " ");
                            }
                            else
                            {
                                reader.Write(secondVector[j++] + " ");
                            }
                        }
                    }
                    if (i == firsVector.Length)
                    {
                        while (j < secondVector.Length)
                        {
                            reader.Write(secondVector[j++] + " ");
                        }
                    }
                    else
                    {
                        while (i < firsVector.Length)
                        {
                            reader.Write(firsVector[i++] + " ");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл не знайдено");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public override string ToString()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("Файл не знайдено");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
