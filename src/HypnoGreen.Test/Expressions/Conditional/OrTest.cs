using System;
using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Conditional;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Test.Expressions.Conditional
{
    [TestFixture]
    public class OrTest
    {
        [Test]
        public void Constructor_NullArgument_ArgumentNullException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Or(null);
            });
        }

        [Test]
        public void Constructor_EmptyArray_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Or(new IExpression[0]);
            });
        }

        [Test]
        public void Evaluate_ThreeTrueExpressions_ReturnsTrue()
        {
            var expression = new Or(
                new Boolean(true),
                new Boolean(true),
                new Boolean(true));
            Assert.True(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_ThreeFalseExpressions_ReturnsFalse()
        {
            var expression = new Or(
                new Boolean(false),
                new Boolean(false),
                new Boolean(false));
            Assert.False(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TwoFalseExpressionsAndOneTrueExpression_ReturnsTrue()
        {
            var expression = new Or(
                new Boolean(false),
                new Boolean(false),
                new Boolean(true));
            Assert.True(expression.EvaluateWithData<bool>());
        }

        [Test]
        public void ToString_TwoTrueExpressionsAndOneFalseExpression_ReturnsExpected()
        {
            var expression = new Or(
                new Boolean(true),
                new Boolean(true),
                new Boolean(false));
            Assert.AreEqual("True || True || False", expression.ToString());
        }
    }
}
