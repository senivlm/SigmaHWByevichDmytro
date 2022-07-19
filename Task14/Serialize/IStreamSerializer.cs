using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task14.Serialize
{
    internal interface IStreamSerializer<T>
    {
        T Deserialize(Stream stream);
        void Serialize(T value, Stream stream);
    }
}
