using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Task11.Enums;
using Task11.Product;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MeatProductParser : ProductParserBase, IStringParser<IMeatProduct>
    {
        public override IMeatProduct Parse(string str)
        {
            try
            {
                Regex namePattern = new("<(Name: )([A-Za-z]+)>");
                Regex pricePattern = new(@"<(Price:) (\d+,\d+|\d+)>");
                Regex weightPattern = new(@"<(Weight:) (\d+,\d+|\d+)>");
                Regex expirationTimePattern = new(@"<(ExpirationTime:) ([0-3][0-9][-./][0-1][0-9][-./][0-9]{4})>");
                Regex meatSpeciesPattern = new(@"<(MeatSpecies:) ([A-Za-z]+)>");
                Regex meatCategoryPattern = new(@"<(MeatCategory:) ([A-Za-z]+)>");
                Regex daysToExpirationAndPresentOfChangePattern = new(@"<(DaysToExpirationAndPresentOfChange:) \{(\((\d+ \d+)\)){0,}\}>");

                string name = namePattern.Match(str).Groups[2].Value;
                double price = double.Parse(pricePattern.Match(str).Groups[2].Value);
                double weight = double.Parse(weightPattern.Match(str).Groups[2].Value);
                DateTime expirationTime = DateTime.Parse(expirationTimePattern.Match(str).Groups[2].Value);
                Enum.TryParse(meatSpeciesPattern.Match(str).Groups[2].Value, out MeatSpecies meatSpecies);
                Enum.TryParse(meatCategoryPattern.Match(str).Groups[2].Value, out MeatCategory meatCategory);

                Group daysToExpirationAndPresentOfChangeTMP = daysToExpirationAndPresentOfChangePattern.Match(str).Groups[2];
                SortedDictionary<int, int> daysToExpirationAndPresentOfChange = new();

                for (int i = 0; i < daysToExpirationAndPresentOfChangeTMP.Captures.Count; i++)
                {
                    string daysAndPresentChangeStr = daysToExpirationAndPresentOfChangeTMP.Captures[i].Value;
                    string[] daysAndPresentChangeSplited = daysAndPresentChangeStr.Trim("()".ToCharArray()).Split();
                    daysToExpirationAndPresentOfChange.TryAdd(int.Parse(daysAndPresentChangeSplited[0]), int.Parse(daysAndPresentChangeSplited[1]));
                }
                return new MeatProductModel(name, price, weight, expirationTime, meatSpecies, meatCategory, daysToExpirationAndPresentOfChange);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
