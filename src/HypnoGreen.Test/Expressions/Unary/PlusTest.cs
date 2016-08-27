using System;
using HypnoGreen.Expressions.Unary;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Unary
{
    [TestFixture]
    public class PlusTest
    {
        [Test]
        public void Evaluate_IntegerOperand_ReturnsInteger()
        {
            var operand = new Integer(3);
            var expression = new Plus(operand);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Evaluate_NumberOperand_ReturnsNumber()
        {
            var operand = new Number(1.5);
            var expression = new Plus(operand);

            var result = expression.EvaluateWithData();

            Assert.AreEqual(1.5, result);
        }

        [Test]
        public void Evaluate_BooleanOperand_ThrowsInvalidOperationException()
        {
            var operand = new Text("Not a Number");
            var expression = new Plus(operand);
            Assert.Throws<InvalidOperationException>(() =>
            {
                expression.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NullOperand_ThrowsInvalidOperationException()
        {
            var operand = Null.Instance;
            var expression = new Minus(operand);
            Assert.Throws<InvalidOperationException>(() =>
            {
                expression.EvaluateWithData();
            });
        }

        [Test]
        public void ToString_IntegerOperand_ReturnsExpected()
        {
            var expr = new Plus(new Integer(3));
            Assert.AreEqual("+3", expr.ToString());
        }
    }
}
