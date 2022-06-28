using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task10
{
    public delegate void NotFoundedInDictionaryHandler<T, G>(string word, ref Dictionary<T, G> dictioanry);
    internal class Translator
    {// Події тут те, що треба.
        public event NotFoundedInDictionaryHandler<string, string> OnNotFoundedInDictionary;
        private string _path;
        private Dictionary<string, string> _dictionary;
        private string _text;
        public Translator()
        {
            _dictionary = new();
        }
        public Translator(Dictionary<string, string> vocubulary, string text) : this()
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
            string result = _text;
            var words = _text.Split(" \t\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
            foreach (string word in words)
            {
                try
                {
                    string tmpResultWord = string.Empty;
                    char punct = default;
                    if (char.IsPunctuation(word[word.Length - 1]))
                    {
                        punct = word[word.Length - 1];
                        tmpResultWord = word[..^1];
                    }
                    else
                    {
                        tmpResultWord = word;
                    }
                    string pattern = $@"\b{tmpResultWord}\b";

                    while (_dictionary.ContainsKey(tmpResultWord.ToLower()) == false)
                    {
                        OnNotFoundedInDictionary(word, ref _dictionary);
                    }

                    string tmpTranslate = _dictionary[tmpResultWord.ToLower()];

                    if (word.ToUpper() == word)
                    {
                        tmpTranslate = tmpTranslate.ToUpper();
                    }
                    else if (char.IsUpper(word[0]))
                    {
                        tmpTranslate = char.ToUpper(tmpTranslate[0]) + tmpTranslate[1..];
                    }

                    result = Regex.Replace(result, pattern, tmpTranslate);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;

        }

    }
}
