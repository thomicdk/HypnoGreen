using System;

namespace HypnoGreen.Expressions.Relational
{
    internal class GreaterThan : RelationalExpression
    {
        public GreaterThan(IExpression leftOperand, IExpression rightOperand) 
            : base(leftOperand, rightOperand, ">")
        { }

        protected override bool Evaluate(IComparable left, IExpression right)
        {
            return left.CompareTo(right) > 0;
        }
    }
}