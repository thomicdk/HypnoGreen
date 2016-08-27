using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Function;
using NUnit.Framework;

namespace HypnoGreen.Test.Expressions.Function
{
    [TestFixture]
    public class SizeTest
    {

        [Test]
        public void Evaluate_Array_ReturnsArrayLength()
        {
            var size = new Size();
            size.AddArgument(new VariableExpression("Persons"));
            var data = new
            {
                Persons = new[] { "Michael", "Anders", "Andreas" }
            };

            var value = size.EvaluateWithData<long>(data);

            Assert.AreEqual(data.Persons.Length, value);
        }

        [Test]
        public void Evaluate2_Array_ReturnsArrayLength()
        {
            IExpression expr = Expression.Parse("Size(Persons)");
            var data = new
            {
                Persons = new[] { "Michael", "Anders", "Andreas" }
            };

            var value = expr.EvaluateWithData<long>(data);

            Assert.AreEqual(data.Persons.Length, value);
        }

        [Test]
        public void ToString_TextArgument_ReturnsExpected()
        {
            var function = new Size();
            function.AddArgument(new HypnoGreen.Types.Text("denmark"));
            Assert.AreEqual("Size(\"denmark\")", function.ToString());
        }
    }
}
