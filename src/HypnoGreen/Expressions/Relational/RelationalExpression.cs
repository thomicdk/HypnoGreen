using System;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Relational
{
    internal abstract class RelationalExpression : BinaryOperatorExpression<Boolean>
    {
        protected RelationalExpression(IExpression leftOperand, IExpression rightOperand, string @operator)
            : base(leftOperand, rightOperand, @operator)
        { }

        protected override Boolean Evaluate(IExpression left, IExpression right)
        {
            var leftComparable = left as IComparable;
            if (leftComparable != null && right != null)
            {
                return new Boolean(Evaluate(leftComparable, right));
            }
            var typeOfRight = right?.GetType().Name ?? "null";
            var exceptionMessage = $"Invalid comparison: {left.GetType().Name} {@operator} {typeOfRight}";
            throw new InvalidOperationException(exceptionMessage);
        }

        protected abstract bool Evaluate(IComparable left, IExpression right);
    }
}
