using System;
using System.Reflection;

namespace HypnoGreen.PropertyPath
{
    internal class Property
    {
        private readonly string _name;

        public Property(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            _name = name;
        }

        internal string Name => _name;

        protected PropertyInfo GetPropertyInfo(Type type)
        {
            var propertyInfo = type.GetTypeInfo().GetProperty(_name);
            if (propertyInfo == null)
                throw new InvalidOperationException($"Property not found in type. Property: ${_name}. Type: ${type.Name}");
            return propertyInfo;
        }

        public virtual Type ResolveType(Type type)
        {
            if (_name == "") return type;
            var propertyInfo = GetPropertyInfo(type);
            return propertyInfo.PropertyType;
        }

        public virtual object ResolveValue(object data, IPropertyValueResolver propertyValueResolver)
        {
            return propertyValueResolver.ResolveProperty(this, data);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
