using System;
using HypnoGreen.PropertyPath;
using NUnit.Framework;

namespace HypnoGreen.Test.PropertyPath
{
    [TestFixture]
    public class PropertyTest
    {

        [Test]
        public void Constructor_NullPropertyArgument_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new Property(null);
            });
        }

        #region ResolveType

        [Test]
        public void ResolveType_ExistingPropertyArgument_ReturnsExpectedType()
        {
            var data = new
            {
                Name = "Denmark"
            };
            var property = new Property("Name");
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(string), type);
        }

        [Test]
        public void ResolveType_PropertyIsNull_ReturnsExpectedType()
        {
            var data = new
            {
                Name = null as string
            };
            var property = new Property("Name");
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(string), type);
        }

        [Test]
        public void ResolveType_PropertyDoesNotExist_ThrowsInvalidOperationException()
        {
            var data = new
            {
                Name = "Denmark"
            };
            var property = new Property("FirstName");
            Assert.Throws<InvalidOperationException>(() =>
            {
                property.ResolveType(data.GetType());
            });
        }

        [Test]
        public void ResolveType_PropertyIsArray_ReturnsArrayType()
        {
            var data = new
            {
                Amounts = new[]
                {
                    4126,
                    309,
                    9281
                }
            };
            var property = new Property("Amounts");
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(int[]), type);
        }

        [Test]
        public void ResolveType_PropertyIsEnum_ReturnsEnumType()
        {
            var data = new
            {
                Enum = DateTimeKind.Unspecified
            };
            var property = new Property("Enum");
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(DateTimeKind), type);
        }

        #endregion ResolveType

        #region ResolveValue

        [Test]
        public void ResolveValue_ExistingPropertyArgument_ReturnsExpectedText()
        {
            var data = new
            {
                Name = "Denmark"
            };
            var property = new Property("Name");
            var value = property.ResolveValue(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual(data.Name, value);
        }

        [Test]
        public void ResolveValue_EmptyPath_ReturnsData()
        {
            var data = new
            {
                Name = "Denmark"
            };
            var property = new Property("");
            var value = property.ResolveValue(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual(data, value);
        }

        [Test]
        public void ResolveValue_EnumValue_ReturnsEnumValueName()
        {
            var data = new
            {
                Enum = DateTimeKind.Unspecified 
            };
            var property = new Property("Enum");
            var value = property.ResolveValue(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual("Unspecified", value);
        }

        #endregion ResolveValue

        public void ToString_ReturnsPropertyName()
        {
            var property = new Property("Person");
            Assert.AreEqual("Person", property.ToString());
        }
    }
}
