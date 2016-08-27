using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Function
{
    internal class NotEmpty : IsEmpty
    {
        public new const string Name = "NotEmpty";
        public override string FunctionName => Name;

        protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
        {
            return new Boolean(!base.ExecuteFunction(ctx, arguments).Value);
        }
    }
}
