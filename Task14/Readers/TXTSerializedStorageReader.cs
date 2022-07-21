using Task14.FileHandler;
using Task14.Validators;

namespace Task14.Readers
{
    internal class TXTSerializedStorageReader<T> : IStreamCollectionReader<ProductStorage<T>, T>
        where T : class, IProduct
    {
        public event LoggerOnBadFormat OnBadFormatLogger;
        public TXTSerializedStorageReader()
        { }
        public TXTSerializedStorageReader(LoggerOnBadFormat onBadFormatLogger)
        {
            OnBadFormatLogger += onBadFormatLogger;
        }

        public void ReadCollection(ref ProductStorage<T> obj, StreamReader stream, Dictionary<string, ITXTSerializedParametersParser<T>> validator)
        {

            try
            {
                TXTSerializedLineAnalyzer tXTSerializedLineAnalyzer = new();
                while (stream.EndOfStream == false)
                {
                    string line = stream.ReadLine();
                    if (string.IsNullOrEmpty(line) == false)
                    {
                        TXTSerializedParameters LineParams = tXTSerializedLineAnalyzer.GetTXTSerializedParameters(line);
                        if (LineParams.ContainsKey("Type"))
                        {
                            string type = LineParams["Type"];
                            if (validator.ContainsKey(type))
                            {
                                T tmpRes = validator[type].Parse(LineParams);
                                if (tmpRes is not null)
                                {
                                    obj.Add(tmpRes);
                                }
                            }
                            else
                            {
                                OnBadFormatLogger?.Invoke(line + "< Description: " + "Набір парсерів не має парсера для цього >;");
                            }
                        }
                        else
                        {
                            OnBadFormatLogger?.Invoke(line + "< Description: " + "Хибний формат запису типу даних>;");
                        }
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
