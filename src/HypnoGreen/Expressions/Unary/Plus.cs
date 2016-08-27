using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Unary
{
    internal class Plus : UnaryNumberExpression
    {
        public Plus(IExpression expression) 
            : base(expression, "+")
        { }

        protected override IExpression EvaluateInteger(Integer value)
        {
            return value;
        }

        protected override IExpression EvaluateNumber(Number value)
        {
            return value;
        }
    }
}