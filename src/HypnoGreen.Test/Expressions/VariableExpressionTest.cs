using System;
using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Types;
using Moq;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions
{
    [TestFixture]
    public class VariableExpressionTest
    {

        [Test]
        public void Evaluate_VariableValueResolution_UsesContextToResolveVariable()
        {
            var expr = new VariableExpression("Count");
            var contextMock = new Mock<IExpressionContext>();

            expr.Evaluate(contextMock.Object);

            contextMock.Verify(ctx => ctx.ResolveVariable("Count"), Times.Once);
        }

        [Test]
        public void Evaluate_VariableValueIsNull_ReturnsNull()
        {
            var expr = new VariableExpression("Test");
            var contextMock = new Mock<IExpressionContext>();
            contextMock.Setup(ctx => ctx.ResolveVariable("Test")).Returns(null);
            
            var value = expr.Evaluate<Null>(contextMock.Object).Value;

            Assert.IsNull(value);
        }

        [Test]
        public void Evaluate_ContextIsNull_ThrowsNullReferenceException()
        {
            var expr = new VariableExpression("Test");
            Assert.Throws<NullReferenceException>(() =>
            {
                expr.Evaluate(null);
            });
        }

        [Test]
        public void CanEvaluate_VariableValueResolution_UsesContextToResolveVariable()
        {
            var expr = new VariableExpression("Count");
            var contextMock = new Mock<IExpressionContext>();
            
            expr.CanEvaluate(contextMock.Object);
            
            contextMock.Verify(ctx => ctx.HasVariable("Count"), Times.Once);
        }

        [Test]
        public void CanEvaluate_ContextIsNull_ThrowsNullReferenceException()
        {
            var expr = new VariableExpression("Test");
            Assert.Throws<NullReferenceException>(() =>
            {
                expr.CanEvaluate(null);
            });
        }
    }
}
