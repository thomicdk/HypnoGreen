using System;

namespace HypnoGreen.Utils
{
    public static class ConvertHelper
    {
        public static object ChangeType(object value, TypeCode typeCode)
        {
            var type = Type.GetType("System." + typeCode);
            return Convert.ChangeType(value, type);
        }
    }
}
