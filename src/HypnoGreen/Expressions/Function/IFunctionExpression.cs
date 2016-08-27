namespace HypnoGreen.Expressions.Function
{
    internal interface IFunctionExpression : IExpression
    {
        int ArgumentCount { get; }

        void AddArgument(IExpression argument);
    }
}