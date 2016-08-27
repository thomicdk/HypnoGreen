using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Evaluation;

namespace HypnoGreen.Types
{
    internal abstract class ValueType<TType> : Expression, IValueType
    {
        public abstract TType Value { get; }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            return this;
        }

        public override bool Equals(object obj)
        {
            var constObj = obj as IValueType;
            if (constObj != null)
            {
                return Value.Equals(constObj.GetValue());
            }
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual object GetValue()
        {
            return Value;
        }
    }
}