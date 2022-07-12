using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task13.FileHandler;
using Task13.Persons;

namespace Task13.Parsers
{
    internal class PersonParser<T> : ITXTSerializedParamsParser<T>
        where T : IPerson
    {
        public event LoggerOnBadFormat OnBadFormatLogger;
        public PersonParser()
        {

        }
        public PersonParser(LoggerOnBadFormat logger)
        {
            OnBadFormatLogger += logger;
        }
        public T Parse(TXTSerializedParameters parameters)
        {
            try
            {
                IPerson person = new Person();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);
                ValidateName(ref person, parameters, logDescriptionLine);
                ValidateStatus(ref person, parameters, logDescriptionLine);
                ValidateCoordinate(ref person, parameters, logDescriptionLine);
                ValidateAge(ref person, parameters, logDescriptionLine);
                ValidateTimeService(ref person, parameters, logDescriptionLine);
                if (logDescriptionLine.Length != discriptionConst.Length)
                {
                    logDescriptionLine.Append(" >;");
                    OnBadFormatLogger?.Invoke(parameters.PrimalLine + logDescriptionLine.ToString());
                    return default(T);
                }
                return (T)person;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected virtual void ValidateName(ref IPerson person, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
        {
            if (txtSerializedParams.ContainsKey("Name") == false)
            {
                logDescriptionLine.Append("Не знайдено ім'я; ");
            }
            else
            {
                if (string.IsNullOrEmpty(txtSerializedParams["Name"]))
                {
                    logDescriptionLine.Append("Хибний формат ім'я; ");
                }
                else
                {
                    person.Name = txtSerializedParams["Name"];
                }
            }
        }
        protected virtual void ValidateStatus(ref IPerson person, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
        {
            if (txtSerializedParams.ContainsKey("Status") == false)
            {
                logDescriptionLine.Append("Не знайдено статус; ");
            }
            else
            {
                if (string.IsNullOrEmpty(txtSerializedParams["Status"]))
                {
                    logDescriptionLine.Append("Хибний формат статусу; ");
                }
                else
                {
                    person.Status = txtSerializedParams["Status"];
                }
            }
        }
        protected virtual void ValidateCoordinate(ref IPerson person, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
        {
            if (txtSerializedParams.ContainsKey("Coordinate") == false)
            {
                logDescriptionLine.Append("Не знайдено координат; ");
            }
            else
            {
                if (double.TryParse(txtSerializedParams["Coordinate"], out double resultCoordinate) == false)
                {
                    logDescriptionLine.Append("Хибний формат координат; ");
                }
                else
                {
                    person.Coordinate = resultCoordinate;
                }
            }
        }
        protected virtual void ValidateAge(ref IPerson person, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
        {
            if (txtSerializedParams.ContainsKey("Age") == false)
            {
                logDescriptionLine.Append("Не знайдено вік; ");
            }
            else
            {
                if (int.TryParse(txtSerializedParams["Age"], out int resultAge) == false)
                {
                    logDescriptionLine.Append("Хибний формат віку; ");
                }
                else
                {
                    person.Age = resultAge;
                }
            }
        }
        protected virtual void ValidateTimeService(ref IPerson person, in TXTSerializedParameters txtSerializedParams, in StringBuilder logDescriptionLine)
        {
            if (txtSerializedParams.ContainsKey("TimeService") == false)
            {
                logDescriptionLine.Append("Не знайдено час обслуговування; ");
            }
            else
            {
                if (int.TryParse(txtSerializedParams["TimeService"], out int resultTimeService) == false)
                {
                    logDescriptionLine.Append("Хибний формат часу обслуговування; ");
                }
                else
                {
                    person.TimeService = resultTimeService;
                }
            }
        }
    }
}
