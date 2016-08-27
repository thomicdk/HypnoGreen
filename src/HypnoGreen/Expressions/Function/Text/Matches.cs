using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Function.Text
{
    internal class Matches : FunctionExpression<Boolean>
    {
        public const string Name = "Matches";
        public override string FunctionName => Name;

        public Matches()
            : base(2)
        { }

        protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            var text = (Types.Text)arguments[0];
            var regex = (RegExp)arguments[1];

            return new Boolean(regex.Value.IsMatch(text.Value));
        }

        protected override Func<IExpression, bool>[] ArgumentValidators => _argumentValidators;

        private static readonly Func<IExpression, bool>[] _argumentValidators =
        {
            arg => arg is Types.Text,
            arg => arg is RegExp
        };
    }
}