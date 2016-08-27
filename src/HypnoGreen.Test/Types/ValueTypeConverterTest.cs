using HypnoGreen.Types;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Test.Types
{
    [TestFixture]
    public class ValueTypeConverterTest
    {

        [Test]
        public void ConvertFrom_Boolean_ReturnsBool()
        {
            var value = new Boolean(true);
            var result = ValueTypeConverter.ConvertFrom<bool>(value);
            Assert.True(result);
        }

    }
}
