using System;
using HypnoGreen.Types;
using NUnit.Framework;
using Contains = HypnoGreen.Expressions.Function.Text.Contains;

namespace HypnoGreen.Test.Expressions.Function.Text
{
    [TestFixture]
    public class ContainsTest 
    {

        [TestCase("","")]
        [TestCase("   ", "   ")]
        [TestCase("   ", "")]
        [TestCase("denmark", "mar")]
        [TestCase("  denmark  ", "  d")]
        [TestCase("DENMARK", "den")]
        public void Evaluate_TextAndText_ReturnsTrue(string text, string substring)
        {
            var contains = new Contains();
            contains.AddArgument(new HypnoGreen.Types.Text(text));
            contains.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.True(contains.EvaluateWithData<bool>());
        }

        [TestCase("", "   ")]
        [TestCase("", "denmark")]
        [TestCase("denmark  ", "  den")]
        public void Evaluate_TextAndText_ReturnsFalse(string text, string substring)
        {
            var contains = new Contains();
            contains.AddArgument(new HypnoGreen.Types.Text(text));
            contains.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.False(contains.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TextAndNull_ReturnsFalse()
        {
            var contains = new Contains();
            contains.AddArgument(new HypnoGreen.Types.Text("denmark"));
            contains.AddArgument(Null.Instance);
            Assert.Throws<InvalidOperationException>(() =>
            {
                contains.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NullAndText_ReturnsFalse()
        {
            var contains = new Contains();
            contains.AddArgument(Null.Instance);
            contains.AddArgument(new HypnoGreen.Types.Text("denmark"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                contains.EvaluateWithData();
            });
        }

        [Test]
        public void ToString_TextArgument_ReturnsExpected()
        {
            var function = new Contains();
            function.AddArgument(new HypnoGreen.Types.Text("denmark"));
            function.AddArgument(new HypnoGreen.Types.Text("nma"));
            Assert.AreEqual("Contains(\"denmark\", \"nma\")", function.ToString());
        }
    }
}
