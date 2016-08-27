using System;
using HypnoGreen.Expressions.Function.Text;
using HypnoGreen.Types;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Function.Text
{
    [TestFixture]
    public class MatchesTest 
    {
        [TestCase("", "")]
        [TestCase("denmark", "denmark")]
        [TestCase("DENMARK", "DENMARK")]
        public void Evaluate_TextAndText_ReturnsTrue(string text, string regex)
        {
            var matches = new Matches();
            matches.AddArgument(new HypnoGreen.Types.Text(text));
            matches.AddArgument(new RegExp(regex));
            Assert.True(matches.EvaluateWithData<bool>());
        }

        
        [TestCase("", "denmark")]
        public void Evaluate_TextAndText_ReturnsFalse(string text, string regex)
        {
            var matches = new Matches();
            matches.AddArgument(new HypnoGreen.Types.Text(text));
            matches.AddArgument(new RegExp(regex));
            Assert.False(matches.EvaluateWithData<bool>());
        }

        [Test]
        public void Evaluate_TextAndNull_ReturnsFalse()
        {
            var matches = new Matches();
            matches.AddArgument(new HypnoGreen.Types.Text("denmark"));
            matches.AddArgument(Null.Instance);
            Assert.Throws<InvalidOperationException>(() =>
            {
                matches.EvaluateWithData();
            });
        }

        [Test]
        public void Evaluate_NullAndText_ReturnsFalse()
        {
            var matches = new Matches();
            matches.AddArgument(Null.Instance);
            matches.AddArgument(new RegExp("denmark"));
            Assert.Throws<InvalidOperationException>(() =>
            {
                matches.EvaluateWithData();
            });
        }

        [Test]
        public void ToString_TextArgument_ReturnsExpected()
        {
            var function = new Matches();
            function.AddArgument(new HypnoGreen.Types.Text("denmark"));
            function.AddArgument(new RegExp("[A-Za-z]+"));
            Assert.AreEqual("Matches(\"denmark\", /[A-Za-z]+/)", function.ToString());
        }
    }
}
