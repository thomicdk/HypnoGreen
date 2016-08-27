using System.Text.RegularExpressions;

namespace HypnoGreen.Utils
{
    internal class TokenInfo
    {
        private readonly Regex _regex;
        
        public Regex Regex
        {
            get { return _regex; }
        }

        public TokenInfo(Regex regex)
        {
            this._regex = regex;
        }
    }
}