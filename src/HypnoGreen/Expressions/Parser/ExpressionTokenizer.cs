using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HypnoGreen.Utils;

namespace HypnoGreen.Expressions.Parser
{
    internal class ExpressionTokenizer : Tokenizer<ExpressionToken, ExpressionTokenInfo>
    {
        #region Static initialization

        static readonly List<ExpressionTokenInfo> TokenInfos = new List<ExpressionTokenInfo>(25);

        static ExpressionTokenizer()
        {
            AddTokenInfo(@",", ExpressionTokenType.Comma);
            AddTokenInfo(@"\(", ExpressionTokenType.LeftParen);
            AddTokenInfo(@"\)", ExpressionTokenType.RightParen);
            AddTokenInfo(@"TRUE", ExpressionTokenType.True, RegexOptions.IgnoreCase);
            AddTokenInfo(@"FALSE", ExpressionTokenType.False, RegexOptions.IgnoreCase);

            AddTokenInfo(@"&&|AND", ExpressionTokenType.And, RegexOptions.IgnoreCase);
            AddTokenInfo(@"\|\||OR", ExpressionTokenType.Or, RegexOptions.IgnoreCase);

            AddTokenInfo(@"[a-zA-Z][a-zA-Z0-9_\[\]\.]*", ExpressionTokenType.Identifier);
            // http://stackoverflow.com/questions/5695240/php-regex-to-ignore-escaped-quotes-within-quotes
            AddTokenInfo(@"""[^""\\]*(?:\\.[^""\\]*)*""", ExpressionTokenType.Text);
            AddTokenInfo(@"/[^/\\]*(?:\\.[^/\\]*)*/", ExpressionTokenType.RegexPattern);
            
            AddTokenInfo(@"\d+(?!\.|\d)", ExpressionTokenType.Integer);
            AddTokenInfo(@"\d+\.\d*|\d*\.\d+", ExpressionTokenType.Decimal);

            AddTokenInfo(@"!(?!=)", ExpressionTokenType.Not);
            AddTokenInfo(@"\*", ExpressionTokenType.Multiplication);
            AddTokenInfo(@"/", ExpressionTokenType.Division);
            AddTokenInfo(@"%", ExpressionTokenType.Remainder);

            AddTokenInfo(@"\+", ExpressionTokenType.Plus);
            AddTokenInfo(@"-", ExpressionTokenType.Minus);

            AddTokenInfo(@"<(?!=)", ExpressionTokenType.Less);
            AddTokenInfo(@"<=", ExpressionTokenType.LessEqual);
            AddTokenInfo(@">(?!=)", ExpressionTokenType.Greater);
            AddTokenInfo(@">=", ExpressionTokenType.GreaterEqual);

            AddTokenInfo(@"==", ExpressionTokenType.Equal);
            AddTokenInfo(@"!=", ExpressionTokenType.NotEqual);
        }

        private static void AddTokenInfo(string tokenRegex, ExpressionTokenType propertyPathTokenType, RegexOptions options = RegexOptions.None)
        {
            var regex = ConstructTokenRegex(tokenRegex, options);
            var tokenInfo = new ExpressionTokenInfo(regex, propertyPathTokenType);
            TokenInfos.Add(tokenInfo);
        }
        #endregion

        public IEnumerable<ExpressionToken> Tokenize(string input, ExpressionTokenizationOptions flags = ExpressionTokenizationOptions.None)
        {
            var tokenInfos = TokenInfos;
            if (flags == ExpressionTokenizationOptions.ExcludeRegex)
            {
                tokenInfos = TokenInfos.Where(ti => ti.Type != ExpressionTokenType.RegexPattern).ToList();
            }
            return Tokenize(tokenInfos, input);
        }

        protected override ExpressionToken ConstructToken(ExpressionTokenInfo tokenType, string sequence, int position)
        {
            return new ExpressionToken(tokenType.Type, sequence, position);
        }
    }
}