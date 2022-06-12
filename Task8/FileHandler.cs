namespace Task8_1
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
        public void WriteObject(IFileWriter objectToWrite)
        {
            objectToWrite.WriteToFile(_path);
        }
        public void ReadObject(IFileReader objectToRead)
        {
            objectToRead.ReadFromFile(_path);
        }


    }
}
