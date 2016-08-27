using System;
using HypnoGreen.Types;

namespace HypnoGreen.Expressions
{
    internal abstract class BinaryOperatorNumberExpression : BinaryOperatorExpression<IExpression>
    {
        protected BinaryOperatorNumberExpression(IExpression leftOperand, IExpression rightOperand, string @operator)
            : base(leftOperand, rightOperand, @operator)
        { }

        protected override IExpression Evaluate(IExpression left, IExpression right)
        {
            if (left is Integer && right is Integer)
            {
                return EvaluateInteger((Integer) left, (Integer)right);
            }
            var rightAsNumber = right as Number;
            var leftAsNumber = left as Number;

            if (rightAsNumber == null && right is Integer)
            {
                rightAsNumber = ((Integer)right).ToNumber();
            }
            if (leftAsNumber == null && left is Integer)
            {
                leftAsNumber = ((Integer) left).ToNumber();
            }

            if (leftAsNumber != null && rightAsNumber != null)
            {
                return EvaluateNumber(leftAsNumber, rightAsNumber);
            }
            else
            {
                throw new InvalidOperationException("NaN");
            }
        }

        protected abstract IExpression EvaluateInteger(Integer left, Integer right);

        protected abstract IExpression EvaluateNumber(Number left, Number right);
    }
}