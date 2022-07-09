using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task12_3.Operation
{
    internal class ConstantOperation : IOperation
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public ConstantOperation(string name, int prioprity, Func<double> behavior)
        {
            Name = name;
            Priority = prioprity;
            Behavior = x => behavior();
        }
        public int Priority { get; private set; }

        public OperationHandler Behavior { get; }

        public IEnumerable<double> GetParamsFromStack(ref Stack<double> stack)
        {
            return new List<double>();
        }
        public int CompareTo(IOperation other)
        {
            return Priority.CompareTo(other.Priority);
        }
    }
}
