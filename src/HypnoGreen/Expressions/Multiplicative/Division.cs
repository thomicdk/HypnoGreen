using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Multiplicative
{
    internal class Division : BinaryOperatorNumberExpression
    {
        public Division(IExpression leftOperand, IExpression rightOperand) 
            : base(leftOperand, rightOperand, "/")
        { }

        protected override IExpression EvaluateInteger(Integer left, Integer right)
        {
            return EvaluateNumber(left.ToNumber(), right.ToNumber());
        }

        protected override IExpression EvaluateNumber(Number left, Number right)
        {
            return new Number(left.Value / right.Value);
        }
    }
}