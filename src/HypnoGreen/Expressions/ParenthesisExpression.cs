using System;
using HypnoGreen.Expressions.Evaluation;

namespace HypnoGreen.Expressions
{
    internal class ParenthesisExpression : Expression
    {
        private readonly IExpression _expression;

        public ParenthesisExpression(IExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            _expression = expression;
        }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            return _expression.Evaluate(ctx);
        }

        public override string ToString()
        {
            return "(" + _expression + ")";
        }
    }
}