using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Equality
{
    internal class NotEquals : Equals
    {
        public NotEquals(IExpression leftOperand, IExpression rightOperand)
            : base(leftOperand, rightOperand, "!=")
        { }

        protected override Boolean Evaluate(IExpression left, IExpression right)
        {
            return new Boolean(!base.Evaluate(left, right).Value);
        }
    }
}