using System.Text.RegularExpressions;
using HypnoGreen.Utils;

namespace HypnoGreen.PropertyPath.Parser
{
    internal class PropertyPathTokenInfo : TokenInfo
    {
        private readonly PropertyPathTokenType type;

        public PropertyPathTokenType Type
        {
            get { return type; }
        }

        public PropertyPathTokenInfo(Regex regex, PropertyPathTokenType type)
            : base(regex)
        {
            this.type = type;
        }
    }
}