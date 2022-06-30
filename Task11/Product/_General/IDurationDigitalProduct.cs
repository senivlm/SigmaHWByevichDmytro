using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Product.General
{
    internal interface IDurationDigitalProduct : IDigitalProduct
    {
        TimeSpan Duration { get; set; }
    }
}
