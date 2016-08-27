using System;
using HypnoGreen.Expressions.Function.Text;
using HypnoGreen.Types;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Test.Expressions.Function.Text
{
    [TestFixture]
    public class StartsWithTest 
    {
        [TestCase("", "")]
        [TestCase("   ", "   ")]
        [TestCase("   ", "")]
        [TestCase("denmark", "den")]
        [TestCase("  denmark  ", "  den")]
        [TestCase("DENMARK", "den")]
        public void Evaluate_TextAndText_ReturnsTrue(string text, string substring)
        {
            var startsWith = new StartsWith();
            startsWith.AddArgument(new HypnoGreen.Types.Text(text));
            startsWith.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.True(startsWith.EvaluateWithData<bool>());
        }
        
        
        [TestCase("", "   ")]
        [TestCase("", "denmark")]
        [TestCase("denmark", "  den")]
        [TestCase("  denmark", "den")]
        public void Evaluate_TextAndText_ReturnsFalse(string text, string substring)
        {
            var startsWith = new StartsWith();
            startsWith.AddArgument(new HypnoGreen.Types.Text(text));
            startsWith.AddArgument(new HypnoGreen.Types.Text(substring));
            Assert.False(startsWith.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TextAndNull_ReturnsFalse()
        {
            var startsWith = new StartsWith();
            startsWith.AddArgument(new HypnoGreen.Types.Text("denmark"));
            startsWith.AddArgument(Null.Instance);
            Assert.Throws<InvalidOperationException>(() =>
            {
                startsWith.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NullAndText_ReturnsFalse()
        {
            var startsWith = new StartsWith();
            startsWith.AddArgument(Null.Instance);
            startsWith.AddArgument(new HypnoGreen.Types.Text("denmark"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                startsWith.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NonStringType_ThrowsInvalidCastException()
        {
            var startsWith = new StartsWith();
            startsWith.AddArgument(new Boolean(false));
            startsWith.AddArgument(new HypnoGreen.Types.Text("denmark"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                startsWith.EvaluateWithData<bool>();
            });
        }

        [Test]
        public void ToString_TextArgument_ReturnsExpected()
        {
            var function = new StartsWith();
            function.AddArgument(new HypnoGreen.Types.Text("denmark"));
            function.AddArgument(new HypnoGreen.Types.Text("den"));
            Assert.AreEqual("StartsWith(\"denmark\", \"den\")", function.ToString());
        }

    }
}
