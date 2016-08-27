using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Unary
{
    internal class Not : UnaryExpression
    {
        public Not(IExpression expression)
            : base(expression, "!")
        { }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            return new Boolean(!Expression.Evaluate<Boolean>(ctx).Value);
        }
    }
}
