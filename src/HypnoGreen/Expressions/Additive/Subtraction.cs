using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Additive
{
    internal class Subtraction : BinaryOperatorNumberExpression
    {
        public Subtraction(IExpression leftOperand, IExpression rightOperand)
            : base(leftOperand, rightOperand, "-")
        { }

        protected override IExpression EvaluateInteger(Integer left, Integer right)
        {
            return new Integer(left.Value - right.Value);
        }

        protected override IExpression EvaluateNumber(Number left, Number right)
        {
            return new Number(left.Value - right.Value);
        }
    }
}