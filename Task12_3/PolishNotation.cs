using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Task12_3
{
    internal class PolishNotation
    {
        private Stack<string> _stack;
        private Operations _operations;
        private string _primalExpressionLine;

        public string PrimalExpressionLine
        {
            get => _primalExpressionLine;
            set => _primalExpressionLine = value;
        }

        public PolishNotation()
        {
            _stack = new Stack<string>();
            _operations = new Operations();
        }
        public PolishNotation(string expression, Operations operationsList)
        {
            _stack = Parse(expression, operationsList)._stack;
            _operations = operationsList;
            _primalExpressionLine = expression;
        }
        public PolishNotation(Stack<string> stack, Operations operations)
        {
            _stack = new(stack);
            _operations = new(operations);
        }
        public double Solve()
        {
            Stack<double> numbers = new();
            foreach (string item in _stack)
            {
                if (double.TryParse(item, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    numbers.Push(number);
                }
                else
                {
                    numbers.Push(_operations[item].Behavior(_operations[item].GetParamsFromStack(numbers).ToArray()));
                }
            }
            return numbers.Pop();
        }

        private static PolishNotation Parse(string expression, Operations operationsList)
        {
            Stack<string> result = new();
            Stack<string> operations = new();
            StringBuilder operation = new();
            StringBuilder digit = new();
            for (int i = 0; i < expression.Length;)
            {
                if (char.IsDigit(expression[i]) || expression[i] == '.')
                {
                    do
                    {
                        digit.Append(expression[i++]);
                    } while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'));
                    result.Push(digit.ToString());
                    digit.Clear();
                }
                else if ("()".Contains(expression[i]) == false)
                {
                    do
                    {
                        operation.Append(expression[i++]);
                    } while (i < expression.Length && char.IsDigit(expression[i]) == false && "()".Contains(expression[i]) == false && operationsList.ContainsKey(operation.ToString()) == false);

                    while (operations.Count > 0 && operations.Peek() != "(" && operationsList[operation.ToString()].CompareTo(operationsList[operations.Peek()]) <= 0)
                    {
                        result.Push(operations.Pop());
                    }
                    operations.Push(operation.ToString());
                    operation.Clear();
                }
                else
                {
                    if (expression[i] == ')')
                    {
                        while (operations.Peek() != "(")
                        {
                            result.Push(operations.Pop());
                        }
                        operations.Pop();
                        i++;
                    }
                    else
                    {
                        operations.Push(expression[i++].ToString());
                    }
                }
            }
            while (operations.Count > 0)
            {
                result.Push(operations.Pop());
            }
            return new PolishNotation(result, operationsList);
        }

        public override string ToString()
        {
            StringBuilder resultString = new();
            foreach (string item in _stack)
            {
                resultString = resultString.Append(item + ' ');
            }
            return resultString.ToString();
        }
    }
}
