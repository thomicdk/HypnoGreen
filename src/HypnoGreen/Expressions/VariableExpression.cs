using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;

namespace HypnoGreen.Expressions
{
    internal class VariableExpression : Expression
    {
        private readonly string _variable;

        public VariableExpression(string variable)
        {
            if (string.IsNullOrWhiteSpace(variable))
                throw new ArgumentException("Non-empty variable name required", nameof(variable));

            _variable = variable;
        }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            if (ctx == null)
                throw new NullReferenceException("Cannot resolve variable without a context");
            var rawValue = ctx.ResolveVariable(_variable);
            var constValue = rawValue as IValueType;
            return constValue ?? ValueTypeConverter.ConvertTo(rawValue);
        }

        public override bool CanEvaluate(IExpressionContext ctx)
        {
            if (ctx == null)
                throw new NullReferenceException("Cannot resolve variable without a context");
            return ctx.HasVariable(_variable);
        }

        public override string ToString()
        {
            return _variable;
        }
    }
}