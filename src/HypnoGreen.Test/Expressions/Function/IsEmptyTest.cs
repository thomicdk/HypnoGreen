using HypnoGreen.Expressions.Function;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Function
{
    [TestFixture]
    public class IsEmptyTest
    {
        private IsEmpty function;

        [SetUp]
        public void SetUp()
        {
            function = new IsEmpty();
        }

        [Test]
        public void Evaluate_NullArgument_ReturnsTrue()
        {
            function.AddArgument(Null.Instance);
            var evalResult = function.EvaluateWithData<bool>();
            Assert.True(evalResult);
        }

        [Test]
        public void Evaluate_EmptyTextArgument_ReturnsTrue()
        {
            function.AddArgument(new HypnoGreen.Types.Text(""));
            var evalResult = function.EvaluateWithData<bool>();
            Assert.True(evalResult);
        }

        [Test]
        public void ToString_IntegerArgument_ReturnsExpected()
        {
            function.AddArgument(new Integer(46));
            Assert.AreEqual("IsEmpty(46)", function.ToString());
        }
    }
}
