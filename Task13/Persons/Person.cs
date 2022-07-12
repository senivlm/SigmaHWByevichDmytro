using System;
using Task13.Enums;

namespace Task13.Persons
{
    internal class Person : IPerson
    {
        private int _age;
        private string _name;
        private Status _status;
        private double _coordinate;

        public Person() : this(default, string.Empty, default, default, default) { }

        public Person(int age, string name, Status status, double coordinate, int timeService)
        {
            Id = Guid.NewGuid();
            _age = age;
            _name = name;
            _status = status;
            _coordinate = coordinate;
            TimeService = timeService;
        }

        public int Age { get => _age; set => _age = value; }
        public string Name { get => _name; set => _name = value; }
        public Status Status { get => _status; set => _status = value; }
        public double Coordinate { get => _coordinate; set => _coordinate = value; }
        public Guid Id { get; }
        public int TimeService { get; set; }

        public int Priority
        {
            get
            {
                int result = 1;
                if (_age <= 6)
                {
                    result+=2;
                }
                if (_age >= 65)
                {
                    result+= 1;
                }
                switch (_status)
                {
                    case Status.NONE:
                        break;
                    case Status.GP_1_DISABILITY:
                        result += 1;
                        break;
                    case Status.GP_2_DISABILITY:
                        result += 2;
                        break;
                    case Status.GP_3_DISABILITY:
                        result += 3;
                        break;
                    case Status.MILITARY:
                        result += 2;
                        break;
                    default:
                        break;
                }
                return result;
            }
        }



        public override string ToString()
        {
            return $"[{_status}] - {_name}: Age: {_age} at coord {_coordinate}";
        }
    }
}
