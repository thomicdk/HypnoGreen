using System;
using System.Linq;
using HypnoGreen.Expressions.Parser;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Parser
{
    [TestFixture]
    internal class ExpressionTokenizerTest
    {
        [TestCase(",", ExpressionTokenType.Comma)]
        [TestCase("(", ExpressionTokenType.LeftParen)]
        [TestCase(")", ExpressionTokenType.RightParen)]
        [TestCase("Person", ExpressionTokenType.Identifier)]
        [TestCase("Person.Name", ExpressionTokenType.Identifier)]
        [TestCase("Person[2].Address[1].PostalCode", ExpressionTokenType.Identifier)]
        [TestCase("\"Michael\"", ExpressionTokenType.Text)]
        [TestCase("\"hyphen (-) is allowed in a string\"", ExpressionTokenType.Text)]
        [TestCase("/[\\d+]*/", ExpressionTokenType.RegexPattern)]
        [TestCase("573", ExpressionTokenType.Integer)]
        [TestCase("4.5", ExpressionTokenType.Decimal)]
        [TestCase("true", ExpressionTokenType.True)]
        [TestCase("false", ExpressionTokenType.False)]
        [TestCase("+", ExpressionTokenType.Plus)]
        [TestCase("-", ExpressionTokenType.Minus)]
        [TestCase("!", ExpressionTokenType.Not)]
        [TestCase("*", ExpressionTokenType.Multiplication)]
        [TestCase("/", ExpressionTokenType.Division)]
        [TestCase("%", ExpressionTokenType.Remainder)]
        [TestCase("&&", ExpressionTokenType.And)]
        [TestCase("AND", ExpressionTokenType.And)]
        [TestCase("||", ExpressionTokenType.Or)]
        [TestCase("OR", ExpressionTokenType.Or)]
        [TestCase("==", ExpressionTokenType.Equal)]
        [TestCase("!=", ExpressionTokenType.NotEqual)]
        [TestCase("<", ExpressionTokenType.Less)]
        [TestCase("<=", ExpressionTokenType.LessEqual)]
        [TestCase(">", ExpressionTokenType.Greater)]
        [TestCase(">=", ExpressionTokenType.GreaterEqual)]
        public void Tokenize_SingleToken_ReturnsExpectedToken(string path, ExpressionTokenType expectedType)
        {
            var tokenizer = new ExpressionTokenizer();
            var token = tokenizer.Tokenize(path).Single();
            Assert.AreEqual(expectedType, token.Type);
        }
        
        [TestCase("$Contact")]
        [TestCase("Contacts{0}")]
        [TestCase("Contacts[0];Name")]
        [TestCase("Cont@cts")]
        public void Tokenize_IllegalCharacters_ThrowsFormatException(string input)
        {
            var tokenizer = new ExpressionTokenizer();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<FormatException>(() =>
            {
                tokenizer.Tokenize(input).ToList(); // Using ToList to force tokenization of the entire string
            });
        }

        [Test]
        public void Tokenize_Null_ThrowsArgumentNullException()
        {
            var tokenizer = new ExpressionTokenizer();
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                tokenizer.Tokenize(null).ToList();
            });
            
        }

    }
}
