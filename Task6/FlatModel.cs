using System;
using System.Collections.Generic;
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
            _datesOfTakingIndicators = new DateTime[4];
        }


        public FlatModel(uint flatNumber, string ownerSurname, double startElectroMeterValue, double endElectroMeterValue, DateTime[] datesOfTakingIndicators)
        {
            FlatNumber = flatNumber;
            OwnerSurname = ownerSurname;
            StartElectroMeterValue = startElectroMeterValue;
            EndElectroMeterValue = endElectroMeterValue;
            _datesOfTakingIndicators = new DateTime[4];
            _datesOfTakingIndicators = datesOfTakingIndicators[..];
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
            stringBuilder.Append($"Номер квартири: {FlatNumber}\t");
            stringBuilder.Append($"Прізвище власника: {OwnerSurname}\t\t");
            stringBuilder.Append($"Вхідні показники: {StartElectroMeterValue:f4}\t");
            stringBuilder.Append($"Вихідні показники: {EndElectroMeterValue:f4}\t");
            stringBuilder.Append($"Дати знаття показчиків: {FlatNumber}\t");
            foreach (var date in _datesOfTakingIndicators)
            {
                stringBuilder.Append($"{date:M}\n");
            }
            return stringBuilder.ToString();           
            
        } 
        #endregion
    }
}
