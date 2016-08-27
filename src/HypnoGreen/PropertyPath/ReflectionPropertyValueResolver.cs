using System;
using System.Linq;
using System.Reflection;

namespace HypnoGreen.PropertyPath
{
    internal class ReflectionPropertyValueResolver : IPropertyValueResolver
    {
        public object ResolveProperty(Property property, object data)
        {
            if (property.Name == "")
                return data;

            var propertyInfo = GetPropertyInfo(property.Name, data.GetType());
            var value = propertyInfo.GetValue(data);
            return ExtractEnumValueNameIfEnum(propertyInfo.PropertyType, value);
        }

        public object ResolveArrayItemProperty(ArrayItemProperty arrayItemProperty, object data)
        {
            var propertyValue = ResolveProperty(arrayItemProperty, data);
            if (propertyValue == null)
                throw new NullReferenceException($"Array property ${arrayItemProperty.Name} is null.");

            Array array = propertyValue as Array;
            if (array == null)
                throw PropertyIsNotAnArrayException(arrayItemProperty, data.GetType());
            var value = array.GetValue(arrayItemProperty.Index);
            return ExtractEnumValueNameIfEnum(array.GetType().GetElementType(), value);
        }

        protected PropertyInfo GetPropertyInfo(string name, Type type)
        {
            var propertyInfo = type.GetTypeInfo().GetProperty(name);
            if (propertyInfo == null)
                throw new InvalidOperationException($"Property not found in type. Property: ${name}. Type: ${type.Name}");
            return propertyInfo;
        }

        protected static object ExtractEnumValueNameIfEnum(Type type, object value)
        {
            if (value == null) return null;

            if (IsEnum(type))
            {
                value = GetEnumValueName(value, type);
            }
            else if (IsArrayOfEnum(type))
            {
                Array array = (Array)value;
                var elementType = array.GetType().GetElementType();
                return array
                    .OfType<object>()
                    .Select(element => GetEnumValueName(element, elementType))
                    .ToArray();
            }
            return value;
        }

        private static bool IsEnum(Type type)
        {
            return type.GetTypeInfo().IsEnum || (type.GetTypeInfo().IsGenericType && type.GenericTypeArguments[0].GetTypeInfo().IsEnum);
        }

        private static object GetEnumValueName(object value, Type type)
        {
            // Special handling for Nullable<Enum>
            var enumType = type.GetTypeInfo().IsGenericType 
                ? type.GenericTypeArguments[0] 
                : type;
            return Enum.GetName(enumType, value);
        }

        private static bool IsArrayOfEnum(Type type)
        {
            return type.IsArray && IsEnum(type.GetElementType());
        }

        private Exception PropertyIsNotAnArrayException(Property property, Type type)
        {
            return new InvalidOperationException($"Property is not an array. Property: ${property.Name}. Type: ${type.Name}");
        }
    }
}