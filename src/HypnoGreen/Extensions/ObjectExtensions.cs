using System.Collections;
using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;

namespace HypnoGreen.Extensions
{
    public static class ObjectExtensions
    {
        public static bool EvaluateBoolExpression(this object obj, string expression)
        {
            return Evaluate<Types.Boolean, bool>(obj, expression);
        }

        public static int EvaluateIntExpression(this object obj, string expression)
        {
            return (int)Evaluate<Integer, long>(obj, expression);
        }

        public static string EvaluateStringExpression(this object obj, string expression)
        {
            return Evaluate<Types.Text, string>(obj, expression);
        }

        private static TBaseType Evaluate<TResult, TBaseType>(object obj, string expression)
            where TResult : ValueType<TBaseType>
        {
            var expr = Expression.Parse(expression);
            var ctx = DetermineContext(obj);
            return expr.Evaluate<TResult>(ctx).Value;
        }

        private static IExpressionContext DetermineContext(object obj)
        {
            var dictionary = obj as IDictionary;
            if (dictionary != null)
            {
                return new DictionaryExpressionContext(dictionary);
            }
            return new DefaultExpressionContext(obj);
        }
    }
}
