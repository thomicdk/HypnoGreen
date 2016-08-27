using HypnoGreen.Utils;

namespace HypnoGreen.Expressions.Parser
{
    internal class ExpressionToken : Token
    {
        private readonly ExpressionTokenType type;

        public ExpressionTokenType Type
        {
            get { return type; }
        }

        public ExpressionToken(ExpressionTokenType type, string sequence, int position) 
            : base(sequence, position)
        {
            this.type = type;
        }

        // ReSharper disable once InconsistentNaming
        private static readonly ExpressionToken epsilon = new ExpressionToken(ExpressionTokenType.Epsilon, null, -1);

        public static ExpressionToken Epsilon
        {
            get { return epsilon; }
        }
    }
}