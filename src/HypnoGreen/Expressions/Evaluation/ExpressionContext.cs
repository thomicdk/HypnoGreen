using System.Globalization;

namespace HypnoGreen.Expressions.Evaluation
{
    public abstract class ExpressionContext : IExpressionContext
    {
        public CultureInfo CultureInfo { get; }
        public bool IgnoreCase { get; }
        
        protected ExpressionContext()
            : this(CultureInfo.InvariantCulture, true)
        { }

        protected ExpressionContext(CultureInfo cultureInfo, bool ignoreCase)
        {
            CultureInfo = cultureInfo;
            IgnoreCase = ignoreCase;
        }

        public abstract object ResolveVariable(string variable);

        public abstract bool HasVariable(string variable);
    }
}
