using HypnoGreen.Expressions.Additive;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Additive
{
    [TestFixture]
    public class AdditionTest
    {

        [Test]
        public void Evaluate_NumberAndInteger_ReturnsNumber()
        {
            var left = new Number(3.5);
            var right = new Integer(5);
            var expression = new Addition(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(8.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndNumber_ReturnsNumber()
        {
            var left = new Integer(5);
            var right = new Number(3.5);
            var expression = new Addition(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(8.5, result);
        }

        [Test]
        public void Evaluate_IntegerAndInteger_ReturnsInteger()
        {
            var left = new Integer(3);
            var right = new Integer(5);
            var expression = new Addition(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(8, result);
        }

        [Test]
        public void Evaluate_NumberAndNumber_ReturnsNumber()
        {
            var left = new Number(3.5);
            var right = new Number(1.2);
            var expression = new Addition(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(4.7, result);
        }

        [Test]
        public void Evaluate_TextAndText_ReturnsText()
        {
            var left = new Text("Hel");
            var right = new Text("lo");
            var expression = new Addition(left, right);

            var result = expression.EvaluateWithData();

            Assert.AreEqual("Hello", result);
        }


        [Test]
        public void ToString_IntegerOperands_ReturnsExpected()
        {
            var expr = new Addition(new Integer(3), new Integer(9));
            Assert.AreEqual("3 + 9", expr.ToString());
        }
    }
}
