using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson16_06
{
    internal class WordNotInDictionaryException:Exception
    {
        public WordNotInDictionaryException()
        {

        }
        public WordNotInDictionaryException(string message):base(message)
        {
        }
    }
}
