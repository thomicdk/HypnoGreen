using HypnoGreen.Expressions;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions
{
    [TestFixture]
    public class ParenthesisExpressionTest
    {

        [Test]
        public void ToString_SurroundedByParenthesis_ReturnsExpected()
        {
            var expr = new ParenthesisExpression(new Integer(4));
            Assert.AreEqual("(4)", expr.ToString());
        }
    }
}
