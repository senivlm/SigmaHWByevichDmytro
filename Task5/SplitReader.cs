using System;
using System.Collections.Generic;
using System.IO;

namespace Task5
{
    internal class SplitReader
    {
        private readonly int _partSize;
        private readonly uint _partsAmount;
        private readonly StreamReader _reader;
        private int _wordCount;
        private int _currentPart;
        public int WordCount => _wordCount;
        public uint PartsAmount => _partsAmount;

        public int CurrentPart => _currentPart;

        public SplitReader(Stream reader, uint partsAmount)
        {
            _reader = new StreamReader(reader);
            GetWordCount();
            reader.Position = 0;

            _reader = new StreamReader(reader);
            _partsAmount = partsAmount;
            _partSize = (int)(_wordCount / partsAmount);
        }
        public bool IsLastPart()
        {
            return _currentPart == _partsAmount;
        }

        public IEnumerable<int> GetNextIntPart()
        {
            _currentPart++;
            List<int> result = new List<int>();
            string word = null;
            int wordCount = 0;
            do
            {
                word = ReadNextWord();
                if (word is not null)
                {
                    wordCount++;
                    if (!int.TryParse(word, out int num))
                    {
                        Console.WriteLine(word + " is not int ");
                    }
                    result.Add(num);
                }
                if (!IsLastPart() && wordCount == _partSize)
                {
                    break;
                }

            } while (word is not null);
            return result;
        }
        public string ReadNextWord()
        {
            bool startRead = false;
            bool endRead = false;
            string word = null;
            while (_reader.Peek() != -1)
            {
                if (endRead)
                {
                    return word;
                }
                char nextChar = (char)_reader.Read();
                if (startRead == false && nextChar != ' ')
                {
                    startRead = true;
                }
                if (startRead == true && endRead == false && _reader.Peek() == ' ')
                {
                    endRead = true;
                }
                if (startRead)
                {
                    word += nextChar != ' ' ? nextChar : null;
                }
            }
            return word;
        }
        private void GetWordCount()
        {
            while (ReadNextWord() is not null)
            {
                _wordCount++;
            }
        }

    }
}
