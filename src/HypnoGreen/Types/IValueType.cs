using HypnoGreen.Expressions;

namespace HypnoGreen.Types
{
    public interface IValueType : IExpression
    {
        object GetValue();
    }
}