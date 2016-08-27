using HypnoGreen.Expressions.Relational;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Relational
{
    [TestFixture]
    public class GreaterThanOrEqualsTest
    {
        [Test]
        public void Evaluate_LeftIsLessThanRight_ReturnsFalse()
        {
            var left = new Integer(-1);
            var right = new Integer(3934);
            var expr = new GreaterThanOrEquals(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void Evaluate_LeftIsEqualToRight_ReturnsTrue()
        {
            var left = new Integer(3934);
            var right = new Integer(3934);
            var expr = new GreaterThanOrEquals(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.True(result);
        }

        [Test]
        public void Evaluate_LeftIsGreaterThanThanRight_ReturnsTrue()
        {
            var left = new Integer(5000);
            var right = new Integer(3934);
            var expr = new GreaterThanOrEquals(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.True(result);
        }

        [Test]
        public void ToString_IntegerComparison_ReturnsExpected()
        {
            var expr = new GreaterThanOrEquals(new Integer(3), new Integer(9));
            Assert.AreEqual("3 >= 9", expr.ToString());
        }
    }
}
