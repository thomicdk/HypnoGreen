using System;
using HypnoGreen.Expressions.Evaluation;

namespace HypnoGreen.Expressions
{
    internal abstract class BinaryOperatorExpression<TResult> : Expression
        where TResult : IExpression
    {
        protected readonly IExpression LeftOperand;
        protected readonly IExpression RightOperand;
        protected readonly string @operator;

        protected BinaryOperatorExpression(IExpression leftOperand, IExpression rightOperand, string @operator)
        {
            if (leftOperand == null) throw new ArgumentNullException(nameof(leftOperand));
            if (rightOperand == null) throw new ArgumentNullException(nameof(rightOperand));

            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            this.@operator = @operator;
        }

        protected abstract TResult Evaluate(IExpression left, IExpression right);

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            var left = LeftOperand.Evaluate(ctx);
            var right = RightOperand.Evaluate(ctx);

            //if (left != null && !(left is TLeftOperand))
            //{
            //    throw new InvalidOperationException("Invalid type: " + left.GetType().Name + ". Expected: " + typeof(TLeftOperand).Name);
            //}
            //if (right != null && !(right is TRightOperand))
            //{
            //    throw new InvalidOperationException("Invalid type: " + right.GetType().Name + ". Expected: " + typeof(TRightOperand).Name);
            //}

            return Evaluate(left, right);
        }

        public override string ToString()
        {
            return LeftOperand + " " + @operator + " " + RightOperand;
        }
    }
}