using System;
using System.Collections.Generic;

namespace Task12_3
{
    internal class BinaryOperation : IOperation
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public BinaryOperation(string name, int prioprity, Func<double, double, double> behavior)
        {
            Name = name;
            Priority = prioprity;
            Behavior = x => behavior(x[0], x[1]);
        }
        public OperationHandler Behavior { get; }

        public int Priority { get; private set; }

        public IEnumerable<double> GetParamsFromStack(Stack<double> stack)
        {
            List<double> result = new List<double>()
            {
                stack.Pop(),
                stack.Pop()
            };
            result.Reverse();
            return result;
        }
        public int CompareTo(IOperation other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}
