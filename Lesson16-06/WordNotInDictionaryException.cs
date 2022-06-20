using System;

namespace Lesson16_06
{
    internal class WordNotInDictionaryException : Exception
    {
        public WordNotInDictionaryException()
        {

        }
        public WordNotInDictionaryException(string message) : base(message)
        {
        }
    }
}
