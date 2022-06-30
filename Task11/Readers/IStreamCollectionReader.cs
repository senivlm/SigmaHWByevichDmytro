using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Validators;

namespace Task11.Readers
{
    internal interface IStreamCollectionReader<T,G> 
        where T : IEnumerable<G>
    {
        void ReadCollection(out T obj, StreamReader stream, Dictionary<string, IStringParser<G>> validator);
    }
}
