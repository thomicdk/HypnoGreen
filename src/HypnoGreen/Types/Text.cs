namespace HypnoGreen.Types
{
    internal class Text : ValueType<string>
    {
        public override string Value { get; }

        public int Length => Value.Length;

        public Text(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $@"""{base.ToString()}""";
        }

        public static Text Empty => new Text(string.Empty);
    }
}