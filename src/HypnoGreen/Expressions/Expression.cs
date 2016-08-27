using System;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Expressions.Parser;

namespace HypnoGreen.Expressions
{
    public abstract class Expression : IExpression
    {
        public abstract IExpression Evaluate(IExpressionContext context);

        public TResult Evaluate<TResult>(IExpressionContext context) where TResult : IExpression
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var obj = Evaluate(context);
            return (TResult)obj;
        }

        public virtual bool CanEvaluate(IExpressionContext ctx)
        {
            return true; // Overwrite to add custom logic
        }
        
        public static IExpression Parse(string expression)
        {
            ExpressionParser parser = new ExpressionParser();
            return parser.Parse(expression);
        }
    }
}
