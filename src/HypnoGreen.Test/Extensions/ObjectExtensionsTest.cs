using HypnoGreen.Extensions;
using NUnit.Framework;

namespace HypnoGreen.Test.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        [Test]
        public void EvaluateBoolExpression_RelationalExpression_ReturnsTrue()
        {
            var data = new
            {
                Name = "Michael",
                Age = 31
            };

            bool isOlderThan30 = data.EvaluateBoolExpression("Age > 30");

            Assert.True(isOlderThan30);
        }

        [Test]
        public void EvaluateIntExpression_AdditionExpression_ReturnsCorrectSum()
        {
            var data = new
            {
                Name = "Michael",
                Age = 31
            };

            var age = data.EvaluateIntExpression("Age + 10");

            Assert.AreEqual(41, age);
        }

    }
}
