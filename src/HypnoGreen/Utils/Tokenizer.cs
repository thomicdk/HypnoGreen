using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HypnoGreen.Utils
{
    internal abstract class Tokenizer<TToken, TTokenInfo>
        where TTokenInfo : TokenInfo
    {

        protected IEnumerable<TToken> Tokenize(List<TTokenInfo> tokenInfos, string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            input = input.Trim();
            var totalLength = input.Length;

            while (input != "") 
            {
                var remaining = input.Length;
                var hasMatch = false;

                for (var i = 0; i < tokenInfos.Count && !hasMatch; i++) {
                    TTokenInfo info = tokenInfos[i];
                    Match m = info.Regex.Match(input);
                    if (m.Success)
                    {
                        hasMatch = true;
                        var sequence = m.Groups["token"].Value.Trim();
                        input = input.Substring(sequence.Length).Trim();

                        var token = ConstructToken(info, sequence, totalLength - remaining);
                        yield return token;
                    }
                }

                if (!hasMatch)
                    throw new FormatException("Unexpected character at col " + (totalLength - input.Length + 1) + ": " + input[0]);
            }
        }

        protected abstract TToken ConstructToken(TTokenInfo tokenType, string sequence, int position);

        protected static Regex ConstructTokenRegex(string tokenRegex, RegexOptions options = RegexOptions.None)
        {
            return new Regex("^(?<token>" + tokenRegex + ")", options);
        }
    }
}
