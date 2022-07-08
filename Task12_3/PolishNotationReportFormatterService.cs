using System.Text;

namespace Task12_3
{
    internal static class PolishNotationReportFormatterService
    {
        public static string CreateReport(PolishNotation polishNotation)
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine($"Початковий вираз: {polishNotation.PrimalExpressionLine} ");
            report.AppendLine($"Польський запис: {polishNotation} ");
            report.AppendLine($"Результат: {polishNotation.Solve()} ");
            return report.ToString();
        }
    }
}
