using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Unary
{
    internal class Minus : UnaryNumberExpression
    {
        public Minus(IExpression expression) 
            : base(expression, "-")
        { }

        protected override IExpression EvaluateInteger(Integer value)
        {
            return new Integer(-value.Value);
        }

        protected override IExpression EvaluateNumber(Number value)
        {
            return new Number(-value.Value);
        }
    }
}