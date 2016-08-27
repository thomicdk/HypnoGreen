using HypnoGreen.Expressions.Equality;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Equality
{
    [TestFixture]
    public class EqualsTest 
    {
        [Test]
        public void Evaluate_LeftIsEqualToRight_ReturnsTrue()
        {
            var left = new Integer(3934);
            var right = new Integer(3934);
            var equals = new Equals(left, right);

            var result = equals.EvaluateWithData<bool>();
            
            Assert.True(result);
        }

        [Test]
        public void Evaluate_LeftIsNotEqualToRight_ReturnsFalse()
        {
            var left = new Integer(-1);
            var right = new Integer(3934);
            var equals = new Equals(left, right);

            var result = equals.EvaluateWithData<bool>();

            Assert.False(result);
        }

        [Test]
        public void Evaluate_LeftIsNull_ReturnsFalse()
        {
            var equals = new Equals(Null.Instance, new Integer(4));
            var result = equals.EvaluateWithData<bool>();
            Assert.False(result);
        }

        [Test]
        public void Evaluate_RightIsNull_ReturnsFalse()
        {
            var equals = new Equals(new Integer(5), Null.Instance);
            var result = equals.EvaluateWithData<bool>();
            Assert.False(result);
        }

        [Test]
        public void Evaluate_LeftAndRightIAreNull_ReturnsTrue()
        {
            var equals = new Equals(Null.Instance, Null.Instance);
            var result = equals.EvaluateWithData<bool>();
            Assert.True(result);
        }

        [Test]
        public void ToString_IntegerComparison_ReturnsExpected()
        {
            var expr = new Equals(new Integer(3), new Integer(9));
            Assert.AreEqual("3 == 9", expr.ToString());
        }
    }
}
