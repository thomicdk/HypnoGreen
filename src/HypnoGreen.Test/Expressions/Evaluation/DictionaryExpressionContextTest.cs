using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HypnoGreen.Expressions.Evaluation;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Evaluation
{
    [TestFixture]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public class DictionaryExpressionContextTest
    {
        [Test]
        public void HasVariable_DictionaryIsNull_ReturnsFalse()
        {
            IDictionary dictionary = null;
            var ctx = new DictionaryExpressionContext(dictionary);
            Assert.False(ctx.HasVariable("name"));
        }

        [Test]
        public void HasVariable_DictionaryWithVariable_ReturnsTrue()
        {
            IDictionary dictionary = new Dictionary<string, string>()
            {
                {"name", "Michael"}
            };
            var ctx = new DictionaryExpressionContext(dictionary);
            Assert.True(ctx.HasVariable("name"));
        }

        [Test]
        public void ResolveVariable_DictionaryIsNull_ReturnsNull()
        {
            IDictionary dictionary = null;
            var ctx = new DictionaryExpressionContext(dictionary);
            var value = ctx.ResolveVariable("name");
            Assert.Null(value);
        }

        [Test]
        public void ResolveVariable_DictionaryWithoutVariable_ReturnsNull()
        {
            IDictionary dictionary = new Dictionary<string, string>()
            {
                {"age", "31"}
            };
            var ctx = new DictionaryExpressionContext(dictionary);
            var value = ctx.ResolveVariable("name");
            Assert.Null(value);
        }

        [Test]
        public void ResolveVariable_DictionaryWithVariable_ReturnsVariableValue()
        {
            IDictionary dictionary = new Dictionary<string, string>()
            {
                {"name", "Michael"}
            };
            var ctx = new DictionaryExpressionContext(dictionary);
            var value = ctx.ResolveVariable("name");
            Assert.AreEqual("Michael", value);
        }

    }
}
