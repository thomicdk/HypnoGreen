using HypnoGreen.Expressions.Multiplicative;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Multiplicative
{
     [TestFixture]
    public class RemainderTest
    {
        [Test]
        public void Evaluate_NumberAndInteger_ReturnsNumber()
        {
            var left = new Number(1.5);
            var right = new Integer(1);
            var expression = new Remainder(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(0.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndNumber_ReturnsNumber()
        {
            var left = new Integer(4);
            var right = new Number(1.5);
            var expression = new Remainder(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void Evaluate_IntegerAndInteger_ReturnsInteger()
        {
            var left = new Integer(5);
            var right = new Integer(3);
            var expression = new Remainder(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Evaluate_NumberAndNumber_ReturnsNumber()
        {
            var left = new Number(5.75);
            var right = new Number(1.5);
            var expression = new Remainder(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(1.25, result);
        }

        [Test]
        public void ToString_IntegerOperands_ReturnsExpected()
        {
            var expr = new Remainder(new Integer(3), new Integer(9));
            Assert.AreEqual("3 % 9", expr.ToString());
        }

    }
}
