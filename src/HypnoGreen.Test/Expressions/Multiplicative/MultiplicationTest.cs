using HypnoGreen.Expressions.Multiplicative;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Multiplicative
{
    [TestFixture]
    public class MultiplicationTest
    {
        [Test]
        public void Evaluate_NumberAndInteger_ReturnsNumber()
        {
            var left = new Number(1.5);
            var right = new Integer(3);
            var expression = new Multiplication(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(4.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndNumber_ReturnsNumber()
        {
            var left = new Integer(3);
            var right = new Number(1.5);
            var expression = new Multiplication(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(4.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndInteger_ReturnsInteger()
        {
            var left = new Integer(3);
            var right = new Integer(5);
            var expression = new Multiplication(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(15, result);
        }

        [Test]
        public void Evaluate_NumberAndNumber_ReturnsNumber()
        {
            var left = new Number(1.5);
            var right = new Number(1.5);
            var expression = new Multiplication(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(2.25, result);
        }

        [Test]
        public void ToString_IntegerOperands_ReturnsExpected()
        {
            var expr = new Multiplication(new Integer(3), new Integer(9));
            Assert.AreEqual("3 * 9", expr.ToString());
        }

    }
}
