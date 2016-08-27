using HypnoGreen.Expressions.Equality;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Equality
{
    [TestFixture]
    public class NotEqualsTest 
    {
        [Test]
        public void Evaluate_LeftIsEqualToRight_ReturnsFalse()
        {
            var left = new Integer(3934);
            var right = new Integer(3934);
            var equals = new NotEquals(left, right);

            var result = equals.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void Evaluate_LeftIsNotEqualToRight_ReturnsTrue()
        {
            var left = new Integer(-1);
            var right = new Integer(3934);
            var equals = new NotEquals(left, right);

            var result = equals.EvaluateWithData<bool>();

            Assert.True(result);
        }

        [Test]
        public void ToString_IntegerComparison_ReturnsExpected()
        {
            var expr = new NotEquals(new Integer(3), new Integer(9));
            Assert.AreEqual("3 != 9", expr.ToString());
        }
    }
}
