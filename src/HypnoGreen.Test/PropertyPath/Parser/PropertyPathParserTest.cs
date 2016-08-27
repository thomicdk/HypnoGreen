using System;
using HypnoGreen.PropertyPath.Parser;
using NUnit.Framework;

namespace HypnoGreen.Test.PropertyPath.Parser
{
    [TestFixture]
    public class PropertyPathParserTest
    {

        [TestCase("")]
        [TestCase("Contact")]
        [TestCase("C452_Hej")]
        [TestCase("Contact[0]")]
        [TestCase("Contact[0].Address.PostalCode")]
        [TestCase("Contact[5].Addresses[13].PostalCode")]
        public void Parse_ValidPath_IsParsedCorrect(string path)
        {
            var parser = new PropertyPathParser();
            var parsedPath = parser.Parse(path);
            Assert.AreEqual(path, parsedPath.ToString());
        }

        [TestCase(".Contact")]
        [TestCase("Contact.")]
        [TestCase(".")]
        [TestCase("452Hej")]
        [TestCase("452")]
        [TestCase("[452]")]
        [TestCase("Contact[")]
        [TestCase("Contact5]")]
        [TestCase("Contact[0.Address.PostalCode")]
        [TestCase("Contact.Addresses[].PostalCode")]
        public void Parse_InValidPath_ThrowsFormatException(string path)
        {
            var parser = new PropertyPathParser();
            Assert.Throws<FormatException>(() =>
            {
                parser.Parse(path);
            });
        }
    }
}
