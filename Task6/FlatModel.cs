using System;
using System.IO;
using System.Text;

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
        public int DaysFromLastCheck
        {
            get { return (int)(DateTime.Today - _datesOfTakingIndicators[2]).TotalDays; }
        }
        public double KilowattDebt => _endElectroMeterValue - _startElectroMeterValue;
        public double EndElectroMeterValue
        {
            get { return _endElectroMeterValue; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Вхідні дані мають від'ємне значення");
                }
                _endElectroMeterValue = value;
            }
        }
        public double StartElectroMeterValue
        {
            get { return _startElectroMeterValue; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Вихідні дані мають від'ємне значення");
                }
                _startElectroMeterValue = value;
            }
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
        #region Methods
        private void DataValidator(string DataLine)
        {
            string exceptionMessage = null;
            string[] flatDataArr = DataLine.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (flatDataArr.Length != 7)
            {
                throw new ArgumentException("Невірна кількість записів у рядку");
            }
            if (!uint.TryParse(flatDataArr[0], out _flatNumber))
            {
                exceptionMessage += $"Невірний формат номеру кімнати: {flatDataArr[0]}; ";
            }
            OwnerSurname = flatDataArr[1];

            if (!double.TryParse(flatDataArr[2], out double tmpStartElectroMeterValue))
            {
                exceptionMessage += $"Невірний формат вхідних показників: {flatDataArr[2]}; ";
            }
            else
            {
                if (tmpStartElectroMeterValue < 0)
                {
                    exceptionMessage += "Вхідні дані мають від'ємне значення";
                }
                else
                {
                    _startElectroMeterValue = tmpStartElectroMeterValue;
                }
            }

            if (!double.TryParse(flatDataArr[3], out double tmpEndElectroMeterValue))
            {
                exceptionMessage += $"Невірний формат вихідних показників: {flatDataArr[3]}; ";
            }
            else
            {
                if (tmpEndElectroMeterValue < 0)
                {
                    exceptionMessage += "Вихідні дані мають від'ємне значення";
                }
                else
                {
                    _endElectroMeterValue = tmpEndElectroMeterValue;
                }
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
        public double GetDebtValue(double kilowattPrice)
        {
            return KilowattDebt * kilowattPrice;
        }
        public bool IsDaysInQuarter(Quarter quarter)
        {
            int startMonth = 1;
            switch (quarter)
            {

                case Quarter.First:
                    startMonth = 1;
                    break;
                case Quarter.Second:
                    startMonth = 4;
                    break;
                case Quarter.Third:
                    startMonth = 7;
                    break;
                case Quarter.Fourth:
                    startMonth = 10;
                    break;
                default:
                    return false;
            }
            foreach (var day in _datesOfTakingIndicators)
            {
                if (day.Month != startMonth++)
                {
                    return false;
                }
            }

            return true;
        }
        public string GetReportFormat()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(string.Format("{0,-15}", $"{FlatNumber} ") + "| ");
            stringBuilder.Append(string.Format("{0,-18}", $"{OwnerSurname} ") + "| ");
            stringBuilder.Append(string.Format("{0,-16}", $"{StartElectroMeterValue} ") + "| ");
            stringBuilder.Append(string.Format("{0,-17}", $"{EndElectroMeterValue} ") + "| ");
            foreach (var date in _datesOfTakingIndicators)
            {
                stringBuilder.Append(string.Format("{0,-15}", $"{date:M} ") + "| ");
            }
            stringBuilder.Append(string.Format("{0,-26}", $"{DaysFromLastCheck} ") + "| ");
            return stringBuilder.ToString();
        }
        #endregion
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
            stringBuilder.Append($"Номер квартири: {FlatNumber}; ");
            stringBuilder.Append($"Прізвище власника: {OwnerSurname}; ");
            stringBuilder.Append($"Вхідні значення: {StartElectroMeterValue}; ");
            stringBuilder.Append($"Вихідні значення: {EndElectroMeterValue}; ");
            stringBuilder.Append($"Перша фіксіція: {_datesOfTakingIndicators[0]:M}; ");
            stringBuilder.Append($"Друга фіксіція: {_datesOfTakingIndicators[1]:M}; ");
            stringBuilder.Append($"Третя фіксіція: {_datesOfTakingIndicators[2]:M}; ");
            stringBuilder.Append($"Днів з останньої фіксації: {DaysFromLastCheck}; ");
            return stringBuilder.ToString();

        }
        #endregion
    }
}
