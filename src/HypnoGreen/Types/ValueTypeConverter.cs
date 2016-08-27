using System;
using System.Collections;
using System.Linq;

namespace HypnoGreen.Types
{
    internal static class ValueTypeConverter
    {
        public static TResult ConvertFrom<TResult>(IValueType valueType) //where TResult : class 
        {
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));
            if (typeof(TResult) == typeof(IValueType))
                return (TResult) valueType;
            return (TResult) valueType.GetValue();
        }

        public static IValueType ConvertTo(object value)
        {
            if (value == null) return Null.Instance;

            var typeCode = Type.GetTypeCode(value.GetType());

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return new Boolean((bool)value);
                case TypeCode.String:
                    return new Text((string)value);
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return new Number((double)value);
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return new Integer((int)value);
                case TypeCode.Object:
                    var array = value as IEnumerable;
                    if (array != null)
                    {
                        var constArray = array.Cast<object>().Select(ConvertTo).ToArray();
                        return new Array(constArray);
                    }
                    break;
            }
            throw new NotSupportedException();
        }
    }
}
