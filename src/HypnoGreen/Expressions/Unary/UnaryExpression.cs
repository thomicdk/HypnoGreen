using System;

namespace HypnoGreen.Expressions.Unary
{
    internal abstract class UnaryExpression : Expression
    {
        protected readonly IExpression Expression;
        private readonly string _operator;

        protected UnaryExpression(IExpression expression, string @operator = "")
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));
            Expression = expression;
            _operator = @operator;
        }

        public override string ToString()
        {
            return _operator + Expression;
        }
    }
}