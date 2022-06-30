using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task8_1
{
    internal class FlatsElectricityDebts : IFileReader, IFileWriter
    {
        #region Props
        private IEnumerable<FlatModel> _flats;
        private uint _faltsAmount;
        private Quarter _quarter;
        public Quarter Quarter => _quarter;
        public uint FlatsAmount => _faltsAmount;
        public int Length => _flats.Count();

        public FlatModel this[int index]
        {
            get
            {
                if (_flats == null || index < 0 || index >= _flats.Count())
                {
                    throw new IndexOutOfRangeException();
                }
                return _flats.ElementAt(index);
            }
        }
        #endregion
        #region Ctors
        public FlatsElectricityDebts()
        {
            _flats = new List<FlatModel>();
            _quarter = Quarter.None;
        }
        public FlatsElectricityDebts(IEnumerable<FlatModel> flats, Quarter quarter)
        {
            _flats = new List<FlatModel>(flats);
            _faltsAmount = (uint)_flats.Count();
            _quarter = quarter;
        }
        public FlatsElectricityDebts(IEnumerable<FlatModel> flats)
        {
            _flats = new List<FlatModel>(flats);
            _faltsAmount = (uint)_flats.Count();
        }
        public FlatsElectricityDebts(string path)
        {
            try
            {
                _flats = new List<FlatModel>();
                ReadFromFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #region Methods
        public void WriteToFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(this.ToString());
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }

        }
        public void ReadFromFile(string filePath)
        {
            List<FlatModel> flats = new List<FlatModel>();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    try
                    {
                        string[] flatsInfo = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (!uint.TryParse(flatsInfo[0], out _faltsAmount))
                        {
                            throw new ArgumentException("Невірний формат кількості записів: " + flatsInfo[0]);
                        }
                        if (!uint.TryParse(flatsInfo[1], out uint quarterNum))
                        {
                            throw new ArgumentException("Невірний формат квартала: " + flatsInfo[0]);
                        }
                        if (quarterNum == 0 || quarterNum > 4)
                        {
                            throw new ArgumentException("Невірний номер квартала: " + quarterNum);
                        }
                        _quarter = (Quarter)quarterNum;
                        while (!reader.EndOfStream)
                        {
                            try
                            {
                                FlatModel flat = new FlatModel(reader);
                                if (!flat.IsDaysInQuarter(_quarter))
                                {
                                    Console.WriteLine($"Дати показань квартири №{flat.FlatNumber} не відповідають кварталу");
                                    continue;
                                }
                                flats.Add(flat);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        _flats = flats;
                    }
                    catch (NullReferenceException)
                    {
                        Console.WriteLine("Файл пустий");
                    }


                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не знайдено");
            }

        }
        public string SelectMaxDebtedSurname()
        {
            if (_flats.Count() == 0)
            {
                Console.WriteLine("Список наразі порожній");
                return null;
            }
            Console.Write("Введіть ціну за КВТ > ");
            if (!double.TryParse(Console.ReadLine(), out double kilowattPrice))
            {
                Console.WriteLine("Невірний формат ціни");
                return null;
            }
            FlatModel debtedFlat = _flats.Where(flat => flat.KilowattDebt == _flats.Select(flat => flat.KilowattDebt).Max()).First();
            return $"Максимальний борг: {String.Format("{0:f4}", debtedFlat.GetDebtValue(kilowattPrice))}, має: {debtedFlat.OwnerSurname}";
        }
        public FlatsElectricityDebts SelectFlatsWithZeroDebt()
        {
            if (_flats.Count() == 0)
            {
                Console.WriteLine("Список наразі порожній");
                return null;
            }
            FlatsElectricityDebts flats = new FlatsElectricityDebts(_flats.Where(flat => flat.KilowattDebt == 0), _quarter);
            return flats;
        }

        public void Add(FlatModel flat)
        {
            _flats = _flats.Concat(new[] { flat });
        }
        #endregion
        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            if (obj is not null && obj is FlatsElectricityDebts other && this.Length == other.Length)
            {
                for (int i = 0; i < this.Length; i++)
                {
                    if (!this[i].Equals(other[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Кількість записів: {_flats.Count()}");
            stringBuilder.AppendLine($"Квартал: {(int)_quarter}");
            stringBuilder.AppendLine("Номер квартири | Прізвище власника | Вхідні значення | Вихідні значення | Перша фіксіція | Друга фіксіція | Третя фіксіція | Днів з останньої фіксації |");
            foreach (FlatModel flat in _flats)
            {
                stringBuilder.AppendLine(flat.GetReportFormat());
            }
            return stringBuilder.ToString();
        }

        #endregion


        public static FlatsElectricityDebts operator +(FlatsElectricityDebts a, FlatsElectricityDebts b)
        {
            FlatsElectricityDebts result = new FlatsElectricityDebts(a._flats);
            for (int i = 0; i < b.Length; i++)
            {
                if (!result._flats.Contains(b[i]))
                {
                    result.Add(b[i]);
                }
            }
            return result;
        }
        public static FlatsElectricityDebts operator -(FlatsElectricityDebts a, FlatsElectricityDebts b)
        {
            FlatsElectricityDebts result = new FlatsElectricityDebts();
            for (int i = 0; i < a.Length; i++)
            {
                if (!b._flats.Contains(a[i]))
                {
                    result.Add(a[i]);
                }
            }
            return result;
        }

    }
}
