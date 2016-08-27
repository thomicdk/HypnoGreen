
namespace HypnoGreen.Expressions.Evaluation
{
    public interface IExpressionEvaluator
    {
        TResult Evaluate<TResult>(string expression, IExpressionContext context) where TResult : IExpression;

        IExpression Evaluate(string expression, IExpressionContext context);

        bool CanEvaluate(string expression, IExpressionContext context);
    }
}
