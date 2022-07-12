using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task13.Enums;

namespace Task13.Persons
{
    internal interface IPerson
    {
        Guid Id { get; }
        int Age { get; set; }
        int TimeService { get; set; }
        string Name { get; set; }
        Status Status { get; set; }
        double Coordinate { get; set; }
        int Priority { get; }
    }
}
