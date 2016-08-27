using System;
using HypnoGreen.PropertyPath.Parser;

namespace HypnoGreen.PropertyPath
{
    public class PropertyPathResolver : IPropertyPathResolver
    {
        public Type GetType(string path, Type fromType)
        {
            return new PropertyPathParser().Parse(path).ResolveType(fromType);
        }
    }
}