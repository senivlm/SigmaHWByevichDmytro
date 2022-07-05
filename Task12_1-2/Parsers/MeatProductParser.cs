using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Task11.Enums;
using Task11.Product;
using Task11.Validators;

namespace Task11.Parsers
{
    internal class MeatProductParser : ProductParserBase, IStringParser<IMeatProduct>
    {
        private Regex _namePattern = new(@"<(Name:) ([^>]*)>");
        private Regex _pricePattern = new(@"<(Price:) ([^>]*)>");
        private Regex _weightPattern = new(@"<(Weight:) ([^>]*)>");
        private Regex _expirationTimePattern = new(@"<(ExpirationTime:) ([^>]*)>");
        private Regex _changePriceByDaysDictionaryPattern = new(@"<(DaysToExpirationAndPresentOfChange:) ([^>]*)>");
        private Regex _changePriceByDaysElementPattern = new(@"{(\([\d]+[\s][\d]+\))*}");
        private Regex _meatSpeciesPattern = new(@"<(MeatSpecies:) ([^>]*)>");
        private Regex _meatCategoryPattern = new(@"<(MeatCategory:) ([^>]*)>");
        public MeatProductParser()
        { }

        public MeatProductParser(LoggerOnBadFormat loggerOnBad) : base(loggerOnBad)
        { }

        public override event LoggerOnBadFormat OnBadFormatLogger;

        public override IMeatProduct Parse(string str)
        {
            try
            {
                IMeatProduct _model = new MeatProductModel();
                string discriptionConst = "<Description: ";
                StringBuilder logDescriptionLine = new(discriptionConst);

                Match nameMatch = _namePattern.Match(str);
                Match priceMatch = _pricePattern.Match(str);
                Match weightMatch = _weightPattern.Match(str);
                Match expirationTimeMatch = _expirationTimePattern.Match(str);
                Match changePriceByDaysDictionaryMatch = _changePriceByDaysDictionaryPattern.Match(str);
                Match meatSpeciesMatch = _meatSpeciesPattern.Match(str);
                Match meatCategoryMatch = _meatCategoryPattern.Match(str);


                #region NameValidate
                if (nameMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено назву продукта; ");
                }
                else
                {
                    if (string.IsNullOrEmpty(nameMatch.Groups[2].Value))
                    {
                        logDescriptionLine.Append("Хибний формат назви; ");
                    }
                    else
                    {
                        _model.Name = nameMatch.Groups[2].Value;
                    }
                }
                #endregion
                #region PriceValidate
                if (priceMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено ціну продукту; ");
                }
                else
                {
                    if (double.TryParse(priceMatch.Groups[2].Value, out double resultPrice) == false)
                    {
                        logDescriptionLine.Append("Хибний формат ціни; ");
                    }
                    else
                    {
                        _model.Price = resultPrice;
                    }
                }
                #endregion
                #region WeightValidate
                if (weightMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено вагу продукту; ");
                }
                else
                {
                    if (double.TryParse(weightMatch.Groups[2].Value, out double resultWeight) == false)
                    {
                        logDescriptionLine.Append("Хибний формат ваги; ");
                    }
                    else
                    {
                        _model.Weight = resultWeight;
                    }
                }
                #endregion
                #region ExpirationTimeValidate
                if (expirationTimeMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено термін придатності продукту; ");
                }
                else
                {
                    if (DateTime.TryParse(expirationTimeMatch.Groups[2].Value, out DateTime resultexpirationTime) == false)
                    {
                        logDescriptionLine.Append("Хибний терміну придатності; ");
                    }
                    else
                    {
                        _model.ExpirationTime = resultexpirationTime;
                    }
                }
                #endregion
                #region ChangePriceByDaysDictionaryValidate
                if (changePriceByDaysDictionaryMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено список змін ціни за терміном придатності; ");
                }
                else
                {
                    Match changePriceByDaysElementMatch = _changePriceByDaysElementPattern.Match(changePriceByDaysDictionaryMatch.Groups[2].Value);
                    if (changePriceByDaysElementMatch.Success == false)
                    {
                        logDescriptionLine.Append("Не знайдено значень у списку змін ціни за терміном придатності; ");
                    }
                    else
                    {
                        SortedDictionary<int, int> changePriceByDaysDictionary = new();
                        foreach (Capture item in changePriceByDaysElementMatch.Groups[1].Captures)
                        {
                            string[] values = item.ToString().Trim("()".ToCharArray()).Split();
                            if (int.TryParse(values[0], out int key) && int.TryParse(values[1], out int value))
                            {
                                if (changePriceByDaysDictionary.TryAdd(key, value) == false)
                                {
                                    logDescriptionLine.Append("Не вдалося додати значення до списку змін ціни за терміном придатності, можливо воно повторюється; ");
                                    break;
                                }
                            }
                            else
                            {
                                logDescriptionLine.Append("Хибний формат значень у списку змін ціни за терміном придатності; ");
                            }
                        }
                        _model.DaysToExpirationAndPresentOfChange = changePriceByDaysDictionary;
                    }
                }
                #endregion
                #region meatSpeciesValidate
                if (meatSpeciesMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено вид м'яса; ");
                }
                else
                {
                    if (Enum.TryParse(meatSpeciesMatch.Groups[2].Value, out MeatSpecies meatSpeciesResult) == false)
                    {
                        logDescriptionLine.Append("Хибний формат виду м'яса; ");
                    }
                    else
                    {
                        _model.MeatSpeciesProp = meatSpeciesResult;
                    }
                }
                #endregion
                #region meatCategoryValidate
                if (meatCategoryMatch.Success == false)
                {
                    logDescriptionLine.Append("Не знайдено категорію м'яса; ");
                }
                else
                {
                    if (Enum.TryParse(meatCategoryMatch.Groups[2].Value, out MeatCategory meatCategoryResult) == false)
                    {
                        logDescriptionLine.Append("Хибний формат категорії м'яса; ");
                    }
                    else
                    {
                        _model.MeatCategoryProp = meatCategoryResult;
                    }
                }
                #endregion

                if (logDescriptionLine.Length != discriptionConst.Length)
                {
                    logDescriptionLine.Append(" >;");
                    OnBadFormatLogger?.Invoke(str + logDescriptionLine.ToString());
                    return null;
                }
                return _model;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
