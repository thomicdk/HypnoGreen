namespace HypnoGreen.Types
{
    internal class Boolean : ValueType<bool>
    {
        public override bool Value { get; }

        public Boolean(bool value)
        {
            Value = value;
        }

        public static Boolean False => new Boolean(false);

        public static Boolean True => new Boolean(true);
    }
}