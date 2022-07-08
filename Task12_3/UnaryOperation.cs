using System;
using System.Collections.Generic;

namespace Task12_3
{
    internal class UnaryOperation : IOperation
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public UnaryOperation(string name, int prioprity, Func<double, double> behavior)
        {
            Name = name;
            Priority = prioprity;
            Behavior = x => behavior(x[0]);
        }
        public int Priority { get; private set; }

        public OperationHandler Behavior { get; }

        public IEnumerable<double> GetParamsFromStack(Stack<double> stack)
        {
            return new List<double>()
            {
                stack.Pop()
            };
        }
        public int CompareTo(IOperation other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}
