using System;

namespace Task13.Persons
{
    internal class Person : IPerson
    {
        private int _age;
        private string _name;
        private string _status;
        private double _coordinate;

        public Person() : this(default, string.Empty, string.Empty, default, default) { }

        public Person(int age, string name, string status, double coordinate, int timeService)
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
        public string Status { get => _status; set => _status = value; }
        public double Coordinate { get => _coordinate; set => _coordinate = value; }
        public Guid Id { get; }
        public int TimeService { get; set; }

        public override string ToString()
        {
            return $"[{_status}] - {_name}: Age: {_age} at coord {_coordinate}";
        }
    }
}
