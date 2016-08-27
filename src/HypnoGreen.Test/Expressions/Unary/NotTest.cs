using System;
using HypnoGreen.Expressions.Conditional;
using HypnoGreen.Expressions.Unary;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Test.Expressions.Unary
{
    [TestFixture]
    public class NotTest
    {

        [Test]
        public void Constructor_NullArgument_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Not(null);
            });
        }

        [Test]
        public void Evaluate_TrueExpression_False()
        {
            var expression = new Not(new Boolean(true));
            Assert.False(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_FalseExpression_True()
        {
            var expression = new Not(new Boolean(false));
            Assert.True(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TrueAndExpression_False()
        {
            var expression = new Not(new And(
                new Boolean(true),
                new Boolean(true),
                new Boolean(true)));
            Assert.False(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_FalseAndExpression_True()
        {
            var expression = new Not(new And(
                new Boolean(true),
                new Boolean(true),
                new Boolean(false)));
            Assert.True(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TrueOrExpression_False()
        {
            var expression = new Not(new Or(
                new Boolean(false),
                new Boolean(false),
                new Boolean(true)));
            Assert.False(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_FalseOrExpression_True()
        {
            var expression = new Not(new Or(
                new Boolean(false),
                new Boolean(false),
                new Boolean(false)));
            Assert.True(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void ToString_BooleanOperand_ReturnsExpected()
        {
            var expr = new Not(new Boolean(true));
            Assert.AreEqual("!True", expr.ToString());
        }
    }
}
