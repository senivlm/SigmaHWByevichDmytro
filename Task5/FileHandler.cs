using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task5
{
    internal class FileHandler
    {
        private string _path;
        public string Path
        {
            get => _path;
            set => _path = value;
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
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return line;
        }

        public void Clear()
        {
            File.WriteAllText(Path, string.Empty);
        }

        public void Copy(FileHandler file)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path))
                {
                    using (Stream reader = File.OpenRead(file.Path))
                    {
                        SplitReader splitReader = new SplitReader(reader, 1);
                        string word = null;
                        do
                        {
                            word = splitReader.ReadNextWord();
                            if (word is null)
                            {
                                break;
                            }
                            writer.Write(word + " ");

                        } while (word is not null);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void WriteToFile(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(Path))
                {
                    writer.Write(data);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AddToFile(string data)
        {
            try
            {
                using (StreamWriter reader = File.AppendText(Path))
                {
                    reader.Write(data);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                        return reader.ReadToEnd().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x));
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Невірний формат запису у файлі");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                };
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
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
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void MergeWriteToFile(Vector firsVector, string tmpVectorFilePath, Trend trend)
        {
            uint i = 0;
            try
            {
                using (StreamWriter reader = new StreamWriter(Path))
                {
                    using (Stream tmpVectorStream = File.OpenRead(tmpVectorFilePath))
                    {
                        SplitReader splitReader = new SplitReader(tmpVectorStream, 1);
                        bool IsReadNextWord = true;
                        int tmpVectorNum = 0;
                        while (i < firsVector.Length)
                        {
                            if (IsReadNextWord)
                            {
                                string tmpVectorElement = splitReader.ReadNextWord();
                                if (!int.TryParse(tmpVectorElement, out tmpVectorNum))
                                {
                                    if (tmpVectorElement is null)
                                    {
                                        break;
                                    }
                                    Console.WriteLine(tmpVectorElement + " is not num");
                                    continue;
                                }
                                IsReadNextWord = false;
                            }


                            if (trend is Trend.increase)
                            {
                                if (firsVector[i] < tmpVectorNum)
                                {
                                    reader.Write(firsVector[i++] + " ");
                                }
                                else
                                {
                                    reader.Write(tmpVectorNum + " ");
                                    IsReadNextWord = true;
                                }
                            }
                            else
                            {
                                if (firsVector[i] > tmpVectorNum)
                                {
                                    reader.Write(firsVector[i++] + " ");
                                }
                                else
                                {
                                    reader.Write(tmpVectorNum + " ");
                                    IsReadNextWord = true;
                                }
                            }
                        }
                        if (i == firsVector.Length)
                        {
                            if (IsReadNextWord == false)
                            {
                                reader.Write(tmpVectorNum + " ");
                            }
                            while (int.TryParse(splitReader.ReadNextWord(), out tmpVectorNum))
                            {
                                reader.Write(tmpVectorNum + " ");
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
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine("Файл не знайдено");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
