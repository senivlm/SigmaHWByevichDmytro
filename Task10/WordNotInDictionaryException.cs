using System;

namespace Task10
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
