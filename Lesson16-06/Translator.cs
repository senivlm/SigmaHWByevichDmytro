using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson16_06
{
    internal class Translator
    {
        private string _path;
        private Dictionary<string, string> _dictionary;
        private string _text;
        public Translator()
        {
            _dictionary = new();
        }
        public Translator(Dictionary<string, string> vocubulary, string text)
        {
            _dictionary = vocubulary;
            _text = text;
        }
        public void SetPath(string path)
        {
            _path = path;
        }
        public void SetDictionary(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }
        public void AddText(string text)
        {
            _text += text;
        }
        public string TranslateWords()
        {
            string result = string.Empty;
            string[] words = _text.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                try
                {
                    if (char.IsPunctuation(word[word.Length - 1]))
                    {
                        char punct = word[word.Length - 1];
                        if (!_dictionary.ContainsKey(word[..^1]))
                        {
                            AddToDictionary(word[..^1]);
                        }
                        result += _dictionary[word[..^1]] + punct + " ";
                    }
                    else
                    {
                        if (!_dictionary.ContainsKey(word))
                        {
                            AddToDictionary(word);
                        }
                        result += _dictionary[word] + " ";
                    }
                }                
                catch (Exception)
                {
                    throw;
                }
                

            }
            return result;

        }
        private void AddToDictionary(string word)
        {
            Console.Write($"Введіть переклад слова {word}> ");            
            _dictionary[word] = Console.ReadLine();
            TextReader.AddToDictionary(_path, word, _dictionary[word]);
        }
    }
}
