using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;
using Array = HypnoGreen.Types.Array;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Function
{
    /// <summary>
    /// Example: "IsEmpty(Person.Name)"
    /// </summary>
    internal class IsEmpty : FunctionExpression<Boolean>
    {
        public const string Name = "IsEmpty";
        public override string FunctionName => Name;

        public IsEmpty() :
            base(1)
        { }

        protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            var value = arguments[0];

            var nullObj = value as Null;
            if (nullObj != null)
            {
                return Boolean.True;
            }

            var array = value as Array;
            if (array != null)
            {
                return new Boolean(array.Length == 0);
            }

            var text = value as Types.Text;
            if (text != null)
            {
                return new Boolean(text.Length == 0);
            }

            return new Boolean(value.ToString().Trim().Length == 0);
        }


        protected override Func<IExpression, bool>[] ArgumentValidators => _argumentValidators;

        private static readonly Func<IExpression, bool>[] _argumentValidators =
        {
            arg => true
        };
    }
}