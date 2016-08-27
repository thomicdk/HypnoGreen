using System;
using System.Linq;
using HypnoGreen.PropertyPath.Parser;
using NUnit.Framework;

namespace HypnoGreen.Test.PropertyPath.Parser
{
    [TestFixture]
    internal class PropertyPathTokenizerTest
    {

        [TestCase(".", PropertyPathTokenType.Dot)]
        [TestCase("0", PropertyPathTokenType.ArrayIndex)]
        [TestCase("452", PropertyPathTokenType.ArrayIndex)]
        [TestCase("Contact", PropertyPathTokenType.Identifier)]
        [TestCase("Contact_1", PropertyPathTokenType.Identifier)]
        [TestCase("C452", PropertyPathTokenType.Identifier)]
        [TestCase("[", PropertyPathTokenType.LeftSquareBracket)]
        [TestCase("]", PropertyPathTokenType.RightSquareBracket)]
        public void Tokenize_SingleToken_ReturnsExpectedToken(string path, PropertyPathTokenType expectedType)
        {
            var tokenizer = new PropertyPathTokenizer();
            var token = tokenizer.Tokenize(path).Single();
            Assert.AreEqual(expectedType, token.Type);
        }

        [TestCase("Contacts(0)")]
        [TestCase("Contacts{0}")]
        [TestCase("Contacts[0],Name")]
        public void Tokenize_IllegalCharacters_ThrowsFormatException(string input)
        {
            var tokenizer = new PropertyPathTokenizer();
            Assert.Throws<FormatException>(() =>
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                tokenizer.Tokenize(input).ToList(); // Using ToList to force tokenization of the entire string    
            });
        }

        [Test]
        public void Tokenize_Null_ThrowsArgumentNullException()
        {
            var tokenizer = new PropertyPathTokenizer();
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                tokenizer.Tokenize(null).ToList();
            });            
        }
    }
}
