using System;
using System.Globalization;

namespace HypnoGreen.Types
{
    /// <summary>Floating point number</summary>
    internal class Number : ValueType<double>, IComparable
    {
        private readonly double _value;

        public override double Value => _value;

        public Number(double value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString(CultureInfo.InvariantCulture);
        }

        public int CompareTo(object obj)
        {
            if (obj is Integer)
            {
                return (int)(Value - ((Integer) obj).Value);
            }
            else if (obj is Number)
            {
                return (int)(Value - ((Number)obj).Value);
            }
            return GetHashCode() - obj.GetHashCode();
        }
    }
}