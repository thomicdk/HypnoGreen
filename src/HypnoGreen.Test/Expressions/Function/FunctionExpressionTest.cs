using System;
using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Evaluation;
using HypnoGreen.Expressions.Function;
using HypnoGreen.Types;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Test.Expressions.Function
{
    [TestFixture]
    public class FunctionExpressionTest
    {
        [Test]
        public void Evaluate_TooFewArguments_ThrowsInvalidOperationException()
        {
            var func = new OneArgumentFunction();
            Assert.Throws<InvalidOperationException>(() =>
            {
                func.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_TooManyArguments_ThrowsInvalidOperationException()
        {
            var func = new OneArgumentFunction();
            func.AddArgument(Null.Instance);
            Assert.Throws<FormatException>(() =>
            {
                func.AddArgument(Null.Instance);
            });
            func.EvaluateWithData();
        }

        private class OneArgumentFunction : FunctionExpression<Boolean>
        {
            public OneArgumentFunction() : base(1)
            { }

            protected override Boolean ExecuteFunction(IExpressionContext ctx, IExpression[] arguments)
            {
                return Boolean.True;
            }

            public override string FunctionName => "OneArgumentFunction";

            protected override Func<IExpression, bool>[] ArgumentValidators
            {
                get { return new Func<IExpression, bool>[] { arg => true }; }
            }
        }
    }

    
}
