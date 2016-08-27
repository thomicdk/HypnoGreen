using System;
using HypnoGreen.Expressions.Function.Text;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Function.Text
{
    [TestFixture]
    public class EndsWithTest
    {
        
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase("   ", "")]
        [TestCase("denmark", "ark")]
        [TestCase("  denmark  ", "ark  ")]
        [TestCase("DENMARK", "ark")]
        public void Evaluate_TextAndText_ReturnsTrue(string text, string substring)
        {
            var endsWith = new EndsWith();
            endsWith.AddArgument(new HypnoGreen.Types.Text(text));
            endsWith.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.True(endsWith.EvaluateWithData<bool>());
        }

        
        [TestCase("", "   ")]
        [TestCase("", "denmark")]
        [TestCase("denmark  ", "ark")]
        public void Evaluate_TextAndText_ReturnsFalse(string text, string substring)
        {
            var endsWith = new EndsWith();
            endsWith.AddArgument(new HypnoGreen.Types.Text(text));
            endsWith.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.False(endsWith.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TextAndNull_ReturnsFalse()
        {
            var endsWith = new EndsWith();
            endsWith.AddArgument(new HypnoGreen.Types.Text("denmark"));
            endsWith.AddArgument(Null.Instance);
            Assert.Throws<InvalidOperationException>(() =>
            {
                endsWith.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NullAndText_ReturnsFalse()
        {
            var endsWith = new EndsWith();
            endsWith.AddArgument(Null.Instance);
            endsWith.AddArgument(new HypnoGreen.Types.Text("denmark"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                endsWith.EvaluateWithData();
            });
        }

        [Test]
        public void ToString_TextArgument_ReturnsExpected()
        {
            var function = new EndsWith();
            function.AddArgument(new HypnoGreen.Types.Text("denmark"));
            function.AddArgument(new HypnoGreen.Types.Text("ark"));
            Assert.AreEqual("EndsWith(\"denmark\", \"ark\")", function.ToString());
        }
    }
}
