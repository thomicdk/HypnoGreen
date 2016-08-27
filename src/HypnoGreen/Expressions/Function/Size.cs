using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;
using Array = HypnoGreen.Types.Array;

namespace HypnoGreen.Expressions.Function
{
    internal class Size : FunctionExpression<Integer>
    {
        public const string Name = "Size";
        public override string FunctionName => Name;
        
        public Size() 
            : base(1)
        { }

        protected override Integer ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            var obj = arguments[0];
            
            var array = obj as Array;
            if (array != null)
            { 
                return new Integer(array.Length);
            }

            var text = obj as Types.Text;
            if (text != null)
            {
                return new Integer(text.Length);
            }

            return new Integer(0);
        }

        protected override Func<IExpression, bool>[] ArgumentValidators => _argumentValidators;

        private static readonly Func<IExpression, bool>[] _argumentValidators =
        {
            arg => arg is Types.Text || arg is Array
        };
    }
}