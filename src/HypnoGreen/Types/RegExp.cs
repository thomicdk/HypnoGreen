using System.Text.RegularExpressions;

namespace HypnoGreen.Types
{
    /// <summary>Regular Expression</summary>
    internal class RegExp : ValueType<Regex>
    {
        public override Regex Value { get; }

        public RegExp(string pattern)
        {
            Value = new Regex(pattern, RegexOptions.ECMAScript | RegexOptions.IgnoreCase);
        }

        public override string ToString()
        {
            return $"/{base.ToString()}/";
        }
    }
}