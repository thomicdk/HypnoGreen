using System;
using System.Collections.Generic;
using System.Linq;
using HypnoGreen.Expressions.Evaluation;

namespace HypnoGreen.Expressions.Function
{
    internal abstract class FunctionExpression<TReturnType> : Expression, IFunctionExpression
        where TReturnType: IExpression
    {
        private readonly List<IExpression> _arguments;

        public int ArgumentCount { get; }

        protected FunctionExpression(int argumentCount)
        {
            ArgumentCount = argumentCount;
            _arguments = new List<IExpression>();
        }

        public void AddArgument(IExpression argument)
        {
            if (argument == null)
                throw new ArgumentNullException(nameof(argument));
            if (_arguments.Count == ArgumentCount)
                throw new FormatException("Too many arguments. Function: " + FunctionName);
            
            _arguments.Add(argument);
        }

        protected abstract TReturnType ExecuteFunction(IExpressionContext ctx, IExpression[] arguments);

        public abstract string FunctionName { get; }

        public override IExpression Evaluate(IExpressionContext ctx)
        {
            if (_arguments.Count < ArgumentCount)
            {
                throw new InvalidOperationException("Too few arguments. Function: " + FunctionName);
            }
            var resolvedArguments = _arguments.Select(arg => arg?.Evaluate(ctx)).ToArray();
            ArgumentTypeValidation(resolvedArguments);
            return ExecuteFunction(ctx, resolvedArguments);
        }

        private void ArgumentTypeValidation(IExpression[] arguments)
        {
            for (var i = 0; i < arguments.Length; i++)
            {
                var argument = arguments[i];
                var validator = ArgumentValidators[i];
                if (argument == null || validator.Invoke(argument) == false)
                {
                    var argumentType = argument?.GetType().Name ?? "NULL";
                    var msg = $"Function {FunctionName}: Invalid argument type: {argumentType}. Argument no. {i+1}";
                    throw new InvalidOperationException(msg);
                }
            }
        }

        protected abstract Func<IExpression, bool>[] ArgumentValidators { get; }

        public override string ToString()
        {
            return FunctionName + "(" + string.Join(", ", _arguments) + ")";
        }
    }
}