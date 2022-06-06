using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6_2
{
    internal class TextHandler
    {
        private string _text;

        public TextHandler() : this("") { }
        public TextHandler(string text)
        {
            _text = text;
        }
        public TextHandler(StreamReader reader)
        {
            _text = reader.ReadToEnd();
        }
        public IEnumerable<string> GetSentences()
        {
            var result = new List<string>();    
            var sentances = _text.Trim().Split(".!?;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var sentance in sentances)
            {
                result.Add(string.Join(" ", sentance.Split(" \t\n\r".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))+".");
            }
            return result;
        }
        public string GetMaxMinWordsInSentences()
        {
            StringBuilder stringBuilder= new StringBuilder();
            IEnumerable<string> sentances = GetSentences();
            foreach (string sentance in sentances)
            {
                var words = sentance.Split(" ,.:<>'\"\\/[]{}()%$#@*_=+-№.".ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
                var minWord = words.Where(word => word.Length == words.Select(word => word.Length).Min()).FirstOrDefault();
                var maxWord = words.Where(word => word.Length == words.Select(word => word.Length).Max()).FirstOrDefault();
                stringBuilder.AppendLine($"Речення: {sentance}");
                stringBuilder.AppendLine($"Найкоротше слово: {minWord}");
                stringBuilder.AppendLine($"Найдовше слово: {maxWord}");
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
        public void WriteSentancesInFile(StreamWriter writer)
        {
            foreach (var sentence in GetSentences())
            {
                writer.WriteLine(sentence);
            }
        }
    }
}
