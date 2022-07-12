using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task13.Persons
{
    internal interface IPerson
    {
        Guid Id { get; }
        int Age { get; set; }
        int TimeService { get; set; }
        string Name { get; set; }
        string Status { get; set; }
        double Coordinate { get; set; }
    }
}
