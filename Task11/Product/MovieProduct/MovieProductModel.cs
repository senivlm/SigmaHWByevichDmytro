using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Product._General;
using Task11.Product.General;

namespace Task11.Product.MovieProduct
{
    internal class MovieProductModel : ProductBase, IMovieProduct
    {
        #region Props
        public string Genre { get; set; }
        public TimeSpan Duration { get; set; }
        public string Link { get; set; }
        public string AuthorName { get; set; }

        #endregion

        #region Ctors
        public MovieProductModel() : 
            this(default, default, default, default, default, default)
        { }

        public MovieProductModel(string name, double price, string genre, TimeSpan duration, string link, string authorName)
        {
            Name = name;
            Price = price;
            Genre = genre;
            Duration = duration;
            Link = link;
            AuthorName = authorName;
        }
        public MovieProductModel(IMovieProduct other) : 
            this(other.Name, other.Price, other.Genre, other.Duration, other.Link, other.AuthorName)
        { }

        #endregion

        #region Methods
        public override object Clone()
        {
            return new MovieProductModel(this);
        }



        #endregion

        #region ObjectOverrides
        public override string ToString()
        {
            return base.ToString()+$"Жанр: {Genre}; Тривалість:{Duration}; Посилання: {Link}; Ім'я Автора: {AuthorName} ";
        }
        #endregion


    }
}
