using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Product.General
{
    internal interface IMovieProduct : IDurationDigitalProduct, IAuthorProduct
    {
        string Genre { get; set; }
    }
}
