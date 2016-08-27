using HypnoGreen.Expressions.Multiplicative;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Multiplicative
{
    [TestFixture]
    public class DivisionTest
    {
        [Test]
        public void Evaluate_NumberAndInteger_ReturnsNumber()
        {
            var left = new Number(4.5);
            var right = new Integer(3);
            var expression = new Division(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(1.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndNumber_ReturnsNumber()
        {
            var left = new Integer(7);
            var right = new Number(3.5);
            var expression = new Division(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Evaluate_IntegerAndInteger_ReturnsNumber()
        {
            var left = new Integer(8);
            var right = new Integer(4);
            var expression = new Division(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(2, result);
        }

        [Test]
        public void Evaluate_NumberAndNumber_ReturnsNumber()
        {
            var left = new Number(4.5);
            var right = new Number(1.5);
            var expression = new Division(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void ToString_IntegerOperands_ReturnsExpected()
        {
            var expr = new Division(new Integer(3), new Integer(9));
            Assert.AreEqual("3 / 9", expr.ToString());
        }
    }
}
