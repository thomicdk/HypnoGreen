using System;
using System.Collections;
using System.Linq;

namespace HypnoGreen.Types
{
    internal class Array : ValueType<IValueType[]>, IEnumerable
    {
        public override IValueType[] Value { get; }

        public Array(IValueType[] array)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            Value = array;
        }

        public int Length => Value.Length;
        
        public IEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        public override object GetValue()
        {
            return Value.Select(v => v.GetValue()).ToArray();
        }
    }
}
