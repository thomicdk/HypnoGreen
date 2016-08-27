using System;

namespace HypnoGreen.PropertyPath
{
    internal class ArrayItemProperty : Property
    {
        private readonly int _index;

        public ArrayItemProperty(string propertyName, int index)
            : base(propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentException("Non-empty property name required", nameof(propertyName));
            if (string.IsNullOrEmpty(propertyName)) throw new ArgumentOutOfRangeException(nameof(index), "Must be >= 0");
            
            _index = index;
        }

        public int Index => _index;

        public override Type ResolveType(Type type)
        {
            var resolvedType = base.ResolveType(type);
            if (!resolvedType.IsArray)
                throw PropertyIsNotAnArrayException(resolvedType);
            return resolvedType.GetElementType();
        }

        public override object ResolveValue(object data, IPropertyValueResolver propertyValueResolver)
        {
            return propertyValueResolver.ResolveArrayItemProperty(this, data);
        }

        private Exception PropertyIsNotAnArrayException(Type type)
        {
            return new InvalidOperationException($"Property is not an array. Property: ${Name}. Type: ${type.Name}");
        }

        public override string ToString()
        {
            return base.ToString() + "[" + _index + "]";
        }
    }
}
