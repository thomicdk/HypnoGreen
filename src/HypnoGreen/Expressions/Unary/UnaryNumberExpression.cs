using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Unary
{
    internal abstract class UnaryNumberExpression : UnaryExpression
    {
        protected UnaryNumberExpression(IExpression expression, string @operator = "") 
            : base(expression, @operator)
        { }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            var value = Expression.Evaluate(ctx);

            if (value is Integer)
            {
                return EvaluateInteger((Integer)value);
            }
            else if (value is Number)
            {
                return EvaluateNumber((Number)value);
            }
            else
            {
                throw new InvalidOperationException("NaN");
            }
        }

        protected abstract IExpression EvaluateInteger(Integer value);
        protected abstract IExpression EvaluateNumber(Number value);
    }
}