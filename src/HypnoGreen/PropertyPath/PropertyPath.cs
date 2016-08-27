using System;

namespace HypnoGreen.PropertyPath
{
    internal class PropertyPath
    {
        private readonly Property _property;
        private readonly PropertyPath _subPath;

        internal PropertyPath(Property property, PropertyPath subPath = null)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
                
            _property = property;
            _subPath = subPath;
        }

        public string Name => _property.Name;

        public Type ResolveType(object data)
        {
            return ResolveType(data.GetType());
        }

        public Type ResolveType(Type type)
        {
            var resolvedType = _property.ResolveType(type);
            if (_subPath != null)
            {
                return _subPath.ResolveType(resolvedType);
            }
            return resolvedType;
        }

        public TValue ResolveValue<TValue>(object data, IPropertyValueResolver propertyValueResolver) where TValue : IConvertible
        {
            var propertyValue = ResolveValue(data, propertyValueResolver);
            var type = typeof(TValue);
            return (TValue)Convert.ChangeType(propertyValue, type);
        }

        public object ResolveValue(object data, IPropertyValueResolver propertyValueResolver)
        {
            var propertyValue = _property.ResolveValue(data, propertyValueResolver);
            if (_subPath != null)
            {
                if (propertyValue == null)
                    throw new NullReferenceException($"Intermediate property ${_property} is null. Path: ${ToString()}");

                return _subPath.ResolveValue(propertyValue, propertyValueResolver);
            }            
            return propertyValue;
        }

        public override string ToString()
        {
            return _property + (_subPath != null ? "." + _subPath : "");
        }
    }
}
