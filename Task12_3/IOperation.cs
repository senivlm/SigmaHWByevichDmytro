using System;
using System.Collections.Generic;

namespace Task12_3
{
    public delegate double OperationHandler(params double[] args);
    internal interface IOperation : IComparable<IOperation>
    {
        int Priority { get; }
        string Name { get; set; }
        OperationHandler Behavior { get; }
        IEnumerable<double> GetParamsFromStack(Stack<double> stack);
    }
}
