using System.Collections.Generic;
using HypnoGreen.Utils;

namespace HypnoGreen.PropertyPath.Parser
{
    internal class PropertyPathTokenizer : Tokenizer<PropertyPathToken, PropertyPathTokenInfo>
    {
        #region Static initialization

        static readonly List<PropertyPathTokenInfo> TokenInfos = new List<PropertyPathTokenInfo>(5);

        static PropertyPathTokenizer()
        {
            AddTokenInfo(@"\.", PropertyPathTokenType.Dot);
            AddTokenInfo(@"\[", PropertyPathTokenType.LeftSquareBracket);
            AddTokenInfo(@"\]", PropertyPathTokenType.RightSquareBracket);
            AddTokenInfo(@"[a-zA-Z][a-zA-Z0-9_]*", PropertyPathTokenType.Identifier);
            AddTokenInfo(@"0|[1-9][0-9]*", PropertyPathTokenType.ArrayIndex);		
        }

        private static void AddTokenInfo(string tokenRegex, PropertyPathTokenType propertyPathTokenType)
        {
            var regex = ConstructTokenRegex(tokenRegex);
            var tokenInfo = new PropertyPathTokenInfo(regex, propertyPathTokenType);
            TokenInfos.Add(tokenInfo);
        }
        #endregion

        public IEnumerable<PropertyPathToken> Tokenize(string input)
        {
            return Tokenize(TokenInfos, input);
        }

        protected override PropertyPathToken ConstructToken(PropertyPathTokenInfo propertyPathTokenType, string sequence, int position)
        {
            return new PropertyPathToken(propertyPathTokenType.Type, sequence, position);
        }
    }
}
