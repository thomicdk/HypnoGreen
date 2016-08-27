using HypnoGreen.Expressions;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions
{
    [TestFixture]
    public class ExpressionTest
    {

        [TestCase("2+4*6", "2+(4*6)")]
        [TestCase("2*4+6", "(2*4)+6")]
        [TestCase("2+4/6", "2+(4/6)")]
        [TestCase("2/4+6", "(2/4)+6")]
        [TestCase("2-4*6", "2-(4*6)")]
        [TestCase("2*4-6", "(2*4)-6")]
        [TestCase("2-4/6", "2-(4/6)")]
        [TestCase("2/4-6", "(2/4)-6")]
        [TestCase("false && false || true", "(false && false) || true")]
        public void Evaluate_OperatorPrecedence(string testExpression, string expectedExpression)
        {
            var expr1 = Expression.Parse(testExpression);
            var expr2 = Expression.Parse(expectedExpression);

            Assert.AreEqual(expr2.EvaluateWithData(), expr1.EvaluateWithData());
        }

        [TestCase("2*3/6", "(2*3)/6")]
        [TestCase("2/3*6", "(2/3)*6")]
        [TestCase("1000/100/10", "(1000/100)/10")]
        [TestCase("60/3/5", "(60/3)/5")]
        public void Evaluate_Operators_AreLeftAssociative(string testExpression, string expectedExpression)
        {
            var expr1 = Expression.Parse(testExpression);
            var expr2 = Expression.Parse(expectedExpression);

            Assert.AreEqual(expr2.EvaluateWithData(), expr1.EvaluateWithData());
        }


        [TestCase("!IsEmpty(\"\")", false)]
        [TestCase("IsEmpty(\"\") == false", false)]
        [TestCase("Contains(\"Michael Thomassen\", \"Thomas\") == (Size(\"Michael Thomassen\") < 20)", true)]
        [TestCase("\"Michael\" + \" \" + \"Thomassen\"", "Michael Thomassen")]
        public void Evaluate_IntegrationTest_ReturnsExpectedValue(string input, object expectedValue)
        {
            var expression = Expression.Parse(input);
            var value = expression.EvaluateWithData();
            Assert.AreEqual(expectedValue, value);
        }
    }
}
