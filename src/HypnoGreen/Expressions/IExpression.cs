using HypnoGreen.Expressions.Evaluation;

namespace HypnoGreen.Expressions
{
    public interface IExpression
    {
        TResult Evaluate<TResult>(IExpressionContext ctx) where TResult : IExpression;

        IExpression Evaluate(IExpressionContext ctx);

        bool CanEvaluate(IExpressionContext ctx);
    }
}