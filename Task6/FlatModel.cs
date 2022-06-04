using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    internal class FlatModel
    {
        #region Props
        private uint _flatNumber;
        private string _ownerSurname;
        private double _startElectroMeterValue;
        private double _endElectroMeterValue;
        private DateTime[] _datesOfTakingIndicators;

        public double EndElectroMeterValue
        {
            get { return _endElectroMeterValue; }
            set { _endElectroMeterValue = value; }
        }

        public double StartElectroMeterValue
        {
            get { return _startElectroMeterValue; }
            set { _startElectroMeterValue = value; }
        }

        public string OwnerSurname
        {
            get { return _ownerSurname; }
            set { _ownerSurname = value; }
        }

        public uint FlatNumber
        {
            get { return _flatNumber; }
            set { _flatNumber = value; }
        }
        #endregion

        #region Ctors
        public FlatModel() : this(default, "", default, default, null)
        {
            _datesOfTakingIndicators = new DateTime[3];
        }

        public FlatModel(uint flatNumber, string ownerSurname, double startElectroMeterValue, double endElectroMeterValue, DateTime[] datesOfTakingIndicators)
        {
            FlatNumber = flatNumber;
            OwnerSurname = ownerSurname;
            StartElectroMeterValue = startElectroMeterValue;
            EndElectroMeterValue = endElectroMeterValue;
            _datesOfTakingIndicators = new DateTime[3];
            _datesOfTakingIndicators = datesOfTakingIndicators[..];
        }
        public FlatModel(StreamReader reader)
        {
            _datesOfTakingIndicators = new DateTime[3];
            DataValidator(reader.ReadLine());
        } 

        #endregion
        private void DataValidator(string DataLine)
        {
            string exceptionMessage = null;            
            string[] flatDataArr = DataLine.Trim().Split(' ',StringSplitOptions.RemoveEmptyEntries);
            if (flatDataArr.Length != 7)
            {
                throw new ArgumentException("Невірна кількість записів у рядку");
            }
            if (!uint.TryParse(flatDataArr[0], out _flatNumber))
            {
                exceptionMessage+=$"Невірний формат номеру кімнати: {flatDataArr[0]}; ";
            }
            OwnerSurname=flatDataArr[1];
            if (!double.TryParse(flatDataArr[2], out _startElectroMeterValue))
            {
                exceptionMessage += $"Невірний формат вхідних показників: {flatDataArr[2]}; ";
            }
            if (!double.TryParse(flatDataArr[3], out _endElectroMeterValue))
            {
                exceptionMessage += $"Невірний формат вихідних показників: {flatDataArr[3]}; ";
            }
            if (!DateTime.TryParse(flatDataArr[4], out _datesOfTakingIndicators[0]))
            {
                exceptionMessage += $"Невірний формат першого місяця квартала: {flatDataArr[4]}; ";
            }
            if (!DateTime.TryParse(flatDataArr[5], out _datesOfTakingIndicators[1]))
            {
                exceptionMessage += $"Невірний формат другого місяця квартала: {flatDataArr[5]}; ";
            }
            if (!DateTime.TryParse(flatDataArr[6], out _datesOfTakingIndicators[2]))
            {
                exceptionMessage += $"Невірний формат третього місяця квартала: {flatDataArr[6]}; ";
            }
            if (exceptionMessage is not null)
            {
                throw new ArgumentException(exceptionMessage);
            }            
        }
        public bool IsDaysInQuarter(Quarter quarter)
        {
            switch (quarter)
            {

                case Quarter.First:
                    foreach (var day in _datesOfTakingIndicators)
                    {
                        if (day.Month>3)
                        {
                            return false;
                        }
                    }
                    break;
                case Quarter.Second:
                    foreach (var day in _datesOfTakingIndicators)
                    {
                        if (day.Month <=3 || day.Month>=7)
                        {
                            return false;
                        }
                    }
                    break;
                case Quarter.Third:
                    foreach (var day in _datesOfTakingIndicators)
                    {
                        if (day.Month <= 6 || day.Month >= 10)
                        {
                            return false;
                        }
                    }
                    break;
                case Quarter.Fourth:
                    foreach (var day in _datesOfTakingIndicators)
                    {
                        if (day.Month < 10)
                        {
                            return false;
                        }
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Номер квартири: {FlatNumber}\t");
            stringBuilder.Append($"Прізвище власника: {OwnerSurname}\t");
            stringBuilder.Append($"Вхідні показники: {StartElectroMeterValue:f4}\t");
            stringBuilder.Append($"Вихідні показники: {EndElectroMeterValue:f4}\t");
            stringBuilder.Append($"Дати знаття показчиків: ");
            foreach (var date in _datesOfTakingIndicators)
            {
                stringBuilder.Append($"{date:M}, ");
            }
            stringBuilder[stringBuilder.Length-2] = ';';
            
            return stringBuilder.ToString();           
            
        } 
        #endregion
    }
}
