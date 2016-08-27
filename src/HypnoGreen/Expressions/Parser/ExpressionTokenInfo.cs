using System.Text.RegularExpressions;
using HypnoGreen.Utils;

namespace HypnoGreen.Expressions.Parser
{
    internal class ExpressionTokenInfo : TokenInfo
    {
        private readonly ExpressionTokenType type;

        public ExpressionTokenType Type
        {
            get { return type; }
        }

        public ExpressionTokenInfo(Regex regex, ExpressionTokenType type)
            : base(regex)
        {
            this.type = type;
        }
    }
}