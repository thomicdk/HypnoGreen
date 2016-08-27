using System;

namespace HypnoGreen.PropertyPath
{
    public interface IPropertyPathResolver
    {
        Type GetType(string path, Type fromType);
    }
}
