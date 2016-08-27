using HypnoGreen.Expressions.Additive;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Additive
{
    [TestFixture]
    public class SubtractionTest
    {
        [Test]
        public void Evaluate_NumberAndInteger_ReturnsNumber()
        {
            var left = new Number(8.5);
            var right = new Integer(5);
            var expression = new Subtraction(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(3.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndNumber_ReturnsNumber()
        {
            var left = new Integer(5);
            var right = new Number(3.5);
            var expression = new Subtraction(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(1.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndInteger_ReturnsInteger()
        {
            var left = new Integer(8);
            var right = new Integer(5);
            var expression = new Subtraction(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Evaluate_NumberAndNumber_ReturnsNumber()
        {
            var left = new Number(3.5);
            var right = new Number(1.2);
            var expression = new Subtraction(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(2.3, result);
        }

        [Test]
        public void ToString_IntegerOperands_ReturnsExpected()
        {
            var expr = new Subtraction(new Integer(3), new Integer(9));
            Assert.AreEqual("3 - 9", expr.ToString());
        }
    }
}
