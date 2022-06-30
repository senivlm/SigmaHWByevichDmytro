using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Task11.Parsers;
using Task11.Product;

namespace Task11.Validators
{
    internal class DairyProductParser : ProductParserBase, IStringParser<DairyProductModel>
    {
        public override DairyProductModel Parse(string str)
        {
            try
            {
                Regex namePattern = new("<(Name: )([A-Za-z]+)>");
                Regex pricePattern = new Regex(@"<(Price:) (\d+,\d+|\d+)>");
                Regex weightPattern = new Regex(@"<(Weight:) (\d+,\d+|\d+)>");
                Regex expirationTimePattern = new Regex(@"<(ExpirationTime:) ([0-3][0-9][-./][0-1][0-9][-./][0-9]{4})>");
                Regex daysToExpirationAndPresentOfChangePattern = new Regex(@"<(DaysToExpirationAndPresentOfChange:) \{(\((\d+ \d+)\)){0,}\}>");

                string name = namePattern.Match(str).Groups[2].Value;
                double price = double.Parse(pricePattern.Match(str).Groups[2].Value);
                double weight = double.Parse(weightPattern.Match(str).Groups[2].Value);
                DateTime expirationTime = DateTime.Parse(expirationTimePattern.Match(str).Groups[2].Value);

                Group daysToExpirationAndPresentOfChangeTMP = daysToExpirationAndPresentOfChangePattern.Match(str).Groups[2];
                SortedDictionary<int, int> daysToExpirationAndPresentOfChange = new SortedDictionary<int, int>();

                for (int i = 0; i < daysToExpirationAndPresentOfChangeTMP.Captures.Count; i++)
                {
                    string daysAndPresentChangeStr = daysToExpirationAndPresentOfChangeTMP.Captures[i].Value;
                    string[] daysAndPresentChangeSplited = daysAndPresentChangeStr.Trim("()".ToCharArray()).Split();
                    daysToExpirationAndPresentOfChange.TryAdd(int.Parse(daysAndPresentChangeSplited[0]), int.Parse(daysAndPresentChangeSplited[1]));
                }
                return new DairyProductModel(name, price, weight, expirationTime, daysToExpirationAndPresentOfChange);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
