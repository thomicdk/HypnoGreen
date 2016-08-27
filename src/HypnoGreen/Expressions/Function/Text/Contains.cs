using System;
using System.Globalization;
using HypnoGreen.Expressions.Evaluation;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Function.Text
{
    internal class Contains : FunctionExpression<Boolean>
    {
        public const string Name = "Contains";
        public override string FunctionName => Name;

        public Contains() 
            : base(2)
        { }

        protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            var text = (Types.Text)arguments[0];
            var substring = (Types.Text)arguments[1];

            return new Boolean(ctx.CultureInfo.CompareInfo.IndexOf(text.Value, substring.Value,
                   ctx.IgnoreCase ? CompareOptions.IgnoreCase : CompareOptions.None) > -1);
        }

        protected override Func<IExpression, bool>[] ArgumentValidators => _argumentValidators;

        private static readonly Func<IExpression, bool>[] _argumentValidators =
        {
            arg => arg is Types.Text,
            arg => arg is Types.Text
        };
    }
}