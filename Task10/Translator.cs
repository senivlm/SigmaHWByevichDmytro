using System;
using System.Collections.Generic;

namespace Task10
{
    public delegate void NotFoundedInDictionaryHandler<T, G>(string word, ref Dictionary<T, G> dictioanry);
    internal class Translator
    {
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
            string result = string.Empty;
            string[] words = _text.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
                    while (_dictionary.ContainsKey(tmpResultWord) == false)
                    {
                        OnNotFoundedInDictionary(word, ref _dictionary);
                    }
                    result += _dictionary[tmpResultWord] + punct + " ";
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
