using HypnoGreen.Expressions.Function;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Function
{
    [TestFixture]
    public class NotEmptyTest
    {
        private NotEmpty _function;

        [SetUp]
        public void SetUp()
        {
            _function = new NotEmpty();
        }

        [Test]
        public void Evaluate_NullArgument_False()
        {
            _function.AddArgument(Null.Instance);
            Assert.False(_function.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_EmptyTextArgument_False()
        {
            _function.AddArgument(HypnoGreen.Types.Text.Empty);
            Assert.False(_function.EvaluateWithData<bool>());
        }

        [Test]
        public void ToString_IntegerArgument_ReturnsExpected()
        {
            _function.AddArgument(new Integer(46));
            Assert.AreEqual("NotEmpty(46)", _function.ToString());
        }
    }
}
