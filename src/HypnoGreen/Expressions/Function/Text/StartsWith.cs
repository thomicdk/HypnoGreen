using System;
using System.Globalization;
using HypnoGreen.Expressions.Evaluation;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Function.Text
{
    internal class StartsWith : FunctionExpression<Boolean>
    {
        public const string Name = "StartsWith";
        public override string FunctionName => Name;

        public StartsWith() : base(2)
        { }

        protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            var text = (Types.Text)arguments[0];
            var prefix = (Types.Text)arguments[1];

            return new Boolean(ctx.CultureInfo.CompareInfo.IsPrefix(text.Value, prefix.Value, ctx.IgnoreCase? CompareOptions.IgnoreCase : CompareOptions.None));
        }

        protected override Func<IExpression, bool>[] ArgumentValidators => _argumentValidators;

        private static readonly Func<IExpression, bool>[] _argumentValidators =
        {
            arg => arg is Types.Text,
            arg => arg is Types.Text
        };
    }
}
