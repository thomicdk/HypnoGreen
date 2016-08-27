using System;
using System.Linq;
using HypnoGreen.Expressions.Evaluation;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Conditional
{
    internal class Or : Expression
    {
        private readonly IExpression[] _expressions;

        public Or(params IExpression[] expressions)
        {
            if (expressions == null || expressions.Length == 0)
                throw new ArgumentException("At least one expression required", nameof(expressions));

            _expressions = expressions;
        }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            return new Boolean(_expressions.Any(expression => expression.Evaluate<Boolean>(ctx).Value));
        }

        public override string ToString()
        {
            return string.Join(" || ", _expressions.Select(e => e.ToString()));
        }
    }
}
