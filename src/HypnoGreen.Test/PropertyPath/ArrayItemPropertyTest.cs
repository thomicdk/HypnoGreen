using System;
using HypnoGreen.PropertyPath;
using NUnit.Framework;

namespace HypnoGreen.Test.PropertyPath
{
    [TestFixture]
    public class ArrayItemPropertyTest
    {
        [Test]
        public void ResolveType_ArrayItem_ReturnsArrayElementType()
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
            var property = new ArrayItemProperty("Amounts", 2);
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(int), type);
        }

        [Test]
        public void ResolveType_EmptyArray_ReturnsArrayElementType()
        {
            var data = new
            {
                Amounts = new int[] { }
            };
            var property = new ArrayItemProperty("Amounts", 2);
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(int), type);
        }

        [Test]
        public void ResolveType_ArrayIsNull_ReturnsArrayElementType()
        {
            var data = new
            {
                Amounts = null as int[]
            };
            var property = new ArrayItemProperty("Amounts", 2);
            var type = property.ResolveType(data.GetType());
            Assert.AreEqual(typeof(int), type);
        }

        [Test]
        public void ResolveType_PropertyDoesNotExist_ThrowsInvalidOperationException()
        {
            var data = new
            {
                Amounts = new [] { 4126 }
            };
            var property = new ArrayItemProperty("stnuoma", 0);
            Assert.Throws<InvalidOperationException>(() =>
            {
                property.ResolveType(data.GetType());
            });
        }

        [Test]
        public void ResolveValue_ArrayItem_ReturnsExpectedValue()
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
            var property = new ArrayItemProperty("Amounts", 2);
            var value = property.ResolveValue(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual(data.Amounts[2], value);
        }

        [Test]
        public void ResolveValue_ArrayOfEnumValues_ReturnsEnumValueName()
        {
            var data = new
            {
                EnumValues = new[]
                {
                    DateTimeKind.Local,
                    DateTimeKind.Unspecified,
                    DateTimeKind.Utc
                }
            };
            var property = new ArrayItemProperty("EnumValues", 1);
            var value = property.ResolveValue(data, new ReflectionPropertyValueResolver());
            Assert.AreEqual("Unspecified", value);
        }

        public void ToString_ReturnsPropertyNameAndArrayIndex()
        {
            var property = new ArrayItemProperty("Person",3);
            Assert.AreEqual("Person[3]", property.ToString());
        }
    }
}
