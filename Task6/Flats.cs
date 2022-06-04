using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    internal class Flats
    {
        private IEnumerable<FlatModel> _flats;
        private uint _faltsAmount;
        private Quarter _quarter;
        public Quarter Quarter
        {
            get { return _quarter; }
        }

        public uint FlatsAmount
        {
            get { return _faltsAmount; }
        }
        public Flats()
        {
            _flats = new List<FlatModel>();
        }
        public Flats(IEnumerable<FlatModel> flats, Quarter quarter)
        {
            _flats = new List<FlatModel>(flats);
            _faltsAmount = (uint)_flats.Count();
            _quarter = quarter;
        }
        public Flats(string path)
        {
            try
            {
                _flats = new List<FlatModel>();
                GetFlatsFromFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetFlatsFromFile(string path)
        {
            var flats = new List<FlatModel>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
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
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }

        #region ObjectOverrides
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var flat in _flats)
            {
                stringBuilder.AppendLine(flat.ToString());
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
