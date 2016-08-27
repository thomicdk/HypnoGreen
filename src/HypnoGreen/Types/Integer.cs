using System;
using System.Globalization;

namespace HypnoGreen.Types
{
    /// <summary>Integral number</summary>
    internal class Integer : ValueType<long>, IComparable
    {
        private readonly long _value;

        public override long Value => _value;

        public Integer(int value) : this((long)value) { }
        
        public Integer(long value)
        {
            _value = value;
        }

        public Number ToNumber()
        {
            return new Number(_value);
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public int CompareTo(object obj)
        {
            if (obj is Integer)
            {
                return (int)(Value - ((Integer)obj).Value);
            }
            else if (obj is Number)
            {
                return (int)(Value - ((Number)obj).Value);
            }
            return GetHashCode() - obj.GetHashCode();
        }
    }
}