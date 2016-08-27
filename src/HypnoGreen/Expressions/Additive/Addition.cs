using HypnoGreen.Types;

namespace HypnoGreen.Expressions.Additive
{
    internal class Addition : BinaryOperatorNumberExpression
    {
        public Addition(IExpression leftOperand, IExpression rightOperand)
            : base(leftOperand, rightOperand, "+")
        { }

        protected override IExpression EvaluateInteger(Integer left, Integer right)
        {
            return new Integer(left.Value + right.Value); 
        }

        protected override IExpression EvaluateNumber(Number left, Number right)
        {
            return new Number(left.Value + right.Value);
        }

        // String concatenation
        protected override IExpression Evaluate(IExpression left, IExpression right)
        {
            if (left is Text && right is Text)
            {
                var newText = ((Text) left).Value + ((Text) right).Value;
                return new Text(newText);
            }
            return base.Evaluate(left, right);
        }
    }
}