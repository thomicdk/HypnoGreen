using HypnoGreen.Expressions.Relational;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Relational
{
    [TestFixture]
    public class LessThanTest
    {
        [Test]
        public void Evaluate_LeftIsLessThanRight_ReturnsTrue()
        {
            var left = new Integer(-1);
            var right = new Integer(3934);
            var expr = new LessThan(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.True(result);
        }

        [Test]
        public void Evaluate_LeftIsEqualToRight_ReturnsFalse()
        {
            var left = new Integer(3934);
            var right = new Integer(3934);
            var expr = new LessThan(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void Evaluate_LeftIsGreaterThanThanRight_ReturnsFalse()
        {
            var left = new Integer(5000);
            var right = new Integer(3934);
            var expr = new LessThan(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void Evaluate_MixTypes_ReturnsFalse()
        {
            var left = new Integer(5000);
            var right = new Number(3934);
            var expr = new LessThan(left, right);

            var result = expr.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void ToString_IntegerComparison_ReturnsExpected()
        {
            var expr = new LessThan(new Integer(3), new Integer(9));
            Assert.AreEqual("3 < 9", expr.ToString());
        }
    }
}
