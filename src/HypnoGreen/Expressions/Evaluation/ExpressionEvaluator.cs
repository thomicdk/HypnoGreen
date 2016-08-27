using HypnoGreen.Expressions.Parser;

namespace HypnoGreen.Expressions.Evaluation
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        public TResult Evaluate<TResult>(string expression, IExpressionContext context) where TResult : IExpression
        {
            ExpressionParser parser = new ExpressionParser();
            return parser.Parse(expression).Evaluate<TResult>(context);
        }

        public IExpression Evaluate(string expression, IExpressionContext context)
        {
            ExpressionParser parser = new ExpressionParser();
            return parser.Parse(expression).Evaluate(context);
        }

        public bool CanEvaluate(string expression, IExpressionContext context)
        {
            return Expression.Parse(expression).CanEvaluate(context);
        }
    }
}