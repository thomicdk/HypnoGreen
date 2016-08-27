using System.Collections;
using System.Globalization;

namespace HypnoGreen.Expressions.Evaluation
{
    public class DictionaryExpressionContext: ExpressionContext
    {
        private readonly IDictionary _dictionary;

        public DictionaryExpressionContext()
            : this(null, CultureInfo.InvariantCulture, true)
        { }

        public DictionaryExpressionContext(IDictionary dictionary)
            : this(dictionary, CultureInfo.InvariantCulture, true)
        { }

        public DictionaryExpressionContext(IDictionary dictionary, CultureInfo cultureInfo, bool ignoreCase)
            : base(cultureInfo, ignoreCase)
        {
            _dictionary = dictionary;
        }

        public override object ResolveVariable(string variable)
        {
            return HasVariable(variable) ? _dictionary[variable] : null;
        }

        public override bool HasVariable(string variable)
        {
            return _dictionary != null && _dictionary.Contains(variable);
        }
    }
}
