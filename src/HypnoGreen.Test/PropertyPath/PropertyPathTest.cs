using System;
using HypnoGreen.PropertyPath;
using HypnoGreen.PropertyPath.Parser;
using NUnit.Framework;

namespace HypnoGreen.Test.PropertyPath
{
    [TestFixture]
    public class PropertyPathTest
    {
        
        [Test]
        public void Constructor_NullPropertyArgument_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>((() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new HypnoGreen.PropertyPath.PropertyPath(null);
            }));
        }

        [Test]
        public void ResolveType_NestedType_ReturnsExpectedType()
        {
            var path = new PropertyPathParser().Parse("ClassB.ClassC.Name");
            var type = path.ResolveType(typeof(ClassA));
            Assert.AreEqual(typeof(string), type);
        }

        [Test]
        public void ResolveType_ObjectInstanceWithSubPath_ReturnsExpectedType()
        {
            var data = new
            {
                Order = new
                {
                    Amount = 10
                }
            };
            var path = new PropertyPathParser().Parse("Order.Amount");
            var type = path.ResolveType(data);
            Assert.AreEqual(typeof(int), type);
        }


        [Test]
        public void ResolveValue_Integer_ReturnsInt()
        {
            var path = new HypnoGreen.PropertyPath.PropertyPath(new Property("Count"));
            var data = new
            {
                Count = 10
            };

            var value = path.ResolveValue<long>(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual(10, value);
        }

        [Test]
        public void ResolveValue_HasSubPath_ReturnsExpectedValue()
        {
            var data = new
            {
                Order = new
                {
                    Amount = 10
                }
            };
            var path = new PropertyPathParser().Parse("Order.Amount");
            var value = path.ResolveValue<int>(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual(data.Order.Amount, value);
        }


        [Test]
        public void Evaluate_PropertyIsArray_ReturnsArray()
        {
            var path = new HypnoGreen.PropertyPath.PropertyPath(new Property("Persons"));
            var data = new
            {
                Persons = new[] { "Michael", "Anders", "Andreas" }
            };

            var value = path.ResolveValue(data, new ReflectionPropertyValueResolver());

            Assert.AreEqual(data.Persons, value);
        }

        [Test]
        public void ToString_PropertyConcatenatedWithSubPath()
        {
            var property = new HypnoGreen.PropertyPath.PropertyPath(
                                new Property("Person"), 
                                    new HypnoGreen.PropertyPath.PropertyPath(
                                        new ArrayItemProperty("Address", 2), 
                                            new HypnoGreen.PropertyPath.PropertyPath(
                                                new Property("PostalCode"))));
            Assert.AreEqual("Person.Address[2].PostalCode", property.ToString());
        }

        #region Helper classes

        private class ClassA
        {
            // ReSharper disable once UnusedMember.Local
            public ClassB ClassB { get; set; }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class ClassB
        {
            // ReSharper disable once UnusedMember.Local
            public ClassC ClassC { get; set; }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class ClassC
        {
            // ReSharper disable once UnusedMember.Local
            public string Name { get; set; }
        }

        #endregion Helper classes
    }
}
