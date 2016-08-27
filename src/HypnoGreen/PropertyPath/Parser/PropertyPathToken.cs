using HypnoGreen.Utils;

namespace HypnoGreen.PropertyPath.Parser
{
    internal class PropertyPathToken : Token
    {
        public PropertyPathTokenType Type { get; }

        public PropertyPathToken(PropertyPathTokenType type, string sequence, int position)
            : base(sequence, position)
        {
            Type = type;
        }
        
        public static PropertyPathToken Epsilon { get; } = new PropertyPathToken(PropertyPathTokenType.Epsilon, null, -1);
    }
}
