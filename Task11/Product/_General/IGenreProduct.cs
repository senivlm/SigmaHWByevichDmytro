using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Product._General
{
    internal interface IGenreProduct:IProduct
    {
        string Genre { get; set; }
    }
}
