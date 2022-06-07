using System.IO;

namespace Task7
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
        public void ReadToObject(IFileReader objectToRead)
        {
            try
            {
                using (StreamReader reader = new StreamReader(_path))
                {
                    objectToRead.ReadFromStream(reader);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public void WriteFromObject(IFileWriter objectFromWrite, bool append = false)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path, append))
                {
                    objectFromWrite.WriteToStream(writer);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


    }
}
