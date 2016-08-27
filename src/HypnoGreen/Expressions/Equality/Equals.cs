using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Equality
{
    internal class Equals : BinaryOperatorExpression<Boolean>
    {
        public Equals(IExpression leftOperand, IExpression rightOperand) 
            : this(leftOperand, rightOperand, "==")
        { }

        protected Equals(IExpression leftOperand, IExpression rightOperand, string @operator)
            : base(leftOperand, rightOperand, @operator)
        { }

        protected override Boolean Evaluate(IExpression left, IExpression right)
        {
            return new Boolean(left.Equals(right));
        }
    }
}