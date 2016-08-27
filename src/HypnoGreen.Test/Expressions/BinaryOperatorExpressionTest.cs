using System;
using HypnoGreen.Expressions.Relational;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions
{

    /// <summary>Class is abstract. Mostly tested by by concrete implementations</summary>
    [TestFixture]
    public class BinaryOperatorExpressionTest
    {
        [Test]
        public void Evaluate_LeftOperandIsInvalidType_ThrowsInvalidOperationException()
        {
            var expr = new LessThan(ExpressionTestHelper.NonComparableExpression, new Integer(1));
            Assert.Throws<InvalidOperationException>(() =>
            {
                expr.EvaluateWithData();
            });
        }
    }
}
