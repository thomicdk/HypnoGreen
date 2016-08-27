using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;
using Moq;

namespace HypnoGreen.Test.Expressions
{
    public static class ExpressionTestHelper
    {

        /// <summary>Test helper that set ups a <code>DefaultExpressionContext</code> using the provided data
        /// and evaluates the expression</summary>
        /// <param name="expression"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object EvaluateWithData(this IExpression expression, object data = null)
        {
            var ctx = new DefaultExpressionContext(data);
            var evalResult = expression.Evaluate(ctx);
            var constValue = ((IValueType)evalResult).GetValue();
            if (constValue.GetType().IsPrimitive || constValue is string)
            {
                return constValue;
            }
            return evalResult.ToString();
        }

        /// <summary>Test helper that setup a <code>DefaultExpressionContext</code> using the provided data
        /// and evaluates the expression</summary>
        /// <param name="expression"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal static TResult EvaluateWithData<TResult>(this IExpression expression, object data = null)
        {
            var ctx = new DefaultExpressionContext(data);
            return expression.Evaluate<ValueType<TResult>>(ctx).Value;
        }


        public static IExpression NonComparableExpression
        {
            get
            {
                var expressionMock = new Mock<IExpression>();
                expressionMock.Setup(ex => ex.Evaluate(It.IsAny<IExpressionContext>())).Returns(Null.Instance);
                return expressionMock.Object;
            }
        }
    }
}
