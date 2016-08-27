using System.Globalization;

namespace HypnoGreen.Expressions.Evaluation
{
    public interface IExpressionContext
    {
        CultureInfo CultureInfo { get; }

        bool IgnoreCase { get; }

        object ResolveVariable(string variable);

        bool HasVariable(string variable);
    }
}
