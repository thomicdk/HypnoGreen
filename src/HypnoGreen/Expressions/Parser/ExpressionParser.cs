using System;
using System.Collections.Generic;
using System.Globalization;
using HypnoGreen.Expressions.Additive;
using HypnoGreen.Expressions.Conditional;
using HypnoGreen.Expressions.Equality;
using HypnoGreen.Expressions.Function;
using HypnoGreen.Expressions.Function.Text;
using HypnoGreen.Expressions.Multiplicative;
using HypnoGreen.Expressions.Relational;
using HypnoGreen.Expressions.Unary;
using HypnoGreen.Types;
using Boolean = HypnoGreen.Types.Boolean;

namespace HypnoGreen.Expressions.Parser
{
    /// <summary>
    /// Recursive Descent Parser LL(1)
    /// 
    /// Grammar inspired by C# specification
    /// See: http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-334.pdf
    /// Section 8.5 Expressions.
    /// </summary>
    internal class ExpressionParser
    {
        private static readonly Dictionary<string, Type> Functions = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase)
        {
            { IsEmpty.Name, typeof(IsEmpty) },
            { NotEmpty.Name, typeof(NotEmpty) },
            { Contains.Name, typeof(Contains) },
            { EndsWith.Name, typeof(EndsWith) },
            { Matches.Name, typeof(Matches) },
            { StartsWith.Name, typeof(StartsWith) },
            { Size.Name, typeof(Size) }
        };

        private IEnumerator<ExpressionToken> tokens;

        private readonly Queue<ExpressionToken> priorityTokenQueue = new Queue<ExpressionToken>();

        private ExpressionToken lookAhead;

        public IExpression Parse(string expression)
        {
            var tokenizer = new ExpressionTokenizer();
            tokens = tokenizer.Tokenize(expression).GetEnumerator();
            NextToken();

            var parsedPath = Expression();

            if (lookAhead != ExpressionToken.Epsilon)
                throw UnexpectedTokenException();

            return parsedPath;
        }

        private IExpression Expression()
        {
		    var expr = OrExpression();
		    return expr;
	    }

        private IExpression OrExpression()
        {
		    var expression = AndExpression();
            while (lookAhead.Type == ExpressionTokenType.Or) 
            {
			    NextToken();
                var rightOperand = AndExpression();
			    expression = new Or(expression, rightOperand);
		    }
		    return expression;
	    }

        private IExpression AndExpression()
        {
            var expression = EqualityExpression();
		    while (lookAhead.Type == ExpressionTokenType.And) 
            {
			    NextToken();
                var rightOperand = EqualityExpression();
			    expression = new And(expression, rightOperand);
		    }
		    return expression;
	    }

        private IExpression EqualityExpression()
        {
            var expression = RelationalExpression();
            while ( lookAhead.Type == ExpressionTokenType.Equal || 
                    lookAhead.Type == ExpressionTokenType.NotEqual)
            {
                var type = lookAhead.Type;
                NextToken();
                var rightOperand = RelationalExpression();
                switch (type)
                {
                    case ExpressionTokenType.Equal:
                        expression = new Equals(expression, rightOperand);
                        break;
                    case ExpressionTokenType.NotEqual:
                        expression = new NotEquals(expression, rightOperand);
                        break;
                }     
            }
            return expression;
        }

        private IExpression RelationalExpression() 
        {
            var expression = AdditiveExpression();
            while ( lookAhead.Type == ExpressionTokenType.Less ||
                    lookAhead.Type == ExpressionTokenType.LessEqual ||
                    lookAhead.Type == ExpressionTokenType.Greater ||
                    lookAhead.Type == ExpressionTokenType.GreaterEqual)
            {
                var type = lookAhead.Type;
                NextToken();
                var rightOperand = AdditiveExpression();
                switch (type)
                {
                    case ExpressionTokenType.Less:
                        expression = new LessThan(expression, rightOperand);
                        break;
                    case ExpressionTokenType.LessEqual:
                        expression = new LessThanOrEquals(expression, rightOperand);
                        break;
                    case ExpressionTokenType.Greater:
                        expression = new GreaterThan(expression, rightOperand);
                        break;
                    case ExpressionTokenType.GreaterEqual:
                        expression = new GreaterThanOrEquals(expression, rightOperand);
                        break;
                }
            }
		    return expression;
	    }

        private IExpression AdditiveExpression()
        {
		    var expression = MultiplicativeExpression();
            while ( lookAhead.Type == ExpressionTokenType.Plus || 
                    lookAhead.Type == ExpressionTokenType.Minus)
            {
                var type = lookAhead.Type;
                NextToken();
                var rightOperand = MultiplicativeExpression();
                switch (type)
                {
                    case ExpressionTokenType.Plus:
                        expression = new Addition(expression, rightOperand);
                        break;
                    case ExpressionTokenType.Minus:
                        expression = new Subtraction(expression, rightOperand);
                        break;
                }        
            }
		    return expression;
	    }

        private IExpression MultiplicativeExpression() 
        {
            var expression = UnaryExpression();
            // A regex pattern is unexpected (and not allowed) at this point,
            // so instead we assume to be in the middle of a double division 
            // arithmetic operation, e.g. "60/3/5" where "/3/" is interpreted as a regex pattern.
            // Hence the regex pattern must be split into smaller tokens.
            if (lookAhead.Type == ExpressionTokenType.RegexPattern)
            {
                var regexTokenizer = new ExpressionTokenizer();
                foreach (var token in regexTokenizer.Tokenize(lookAhead.Sequence, ExpressionTokenizationOptions.ExcludeRegex))
                {
                    priorityTokenQueue.Enqueue(token);
                }
                NextToken();
            }

            while ( lookAhead.Type == ExpressionTokenType.Multiplication || 
                    lookAhead.Type == ExpressionTokenType.Division ||
                    lookAhead.Type == ExpressionTokenType.Remainder)
            {
                var type = lookAhead.Type;
                NextToken();
                var rightOperand = UnaryExpression();

                switch (type)
                {
                    case ExpressionTokenType.Multiplication:
                        expression = new Multiplication(expression, rightOperand);
                        break;
                    case ExpressionTokenType.Division:
                        expression = new Division(expression, rightOperand);
                        break;
                    case ExpressionTokenType.Remainder:
                        expression = new Remainder(expression, rightOperand);
                        break;
                }
            }

		    return expression;
	    }

        private IExpression UnaryExpression()
        {
            if (lookAhead.Type == ExpressionTokenType.Plus ||
                lookAhead.Type == ExpressionTokenType.Minus ||
                lookAhead.Type == ExpressionTokenType.Not)
            {
                var type = lookAhead.Type;
                NextToken();
                var atom = Atom();

                switch (type)
                {
                    case ExpressionTokenType.Plus:
                        return new Plus(atom);
                    case ExpressionTokenType.Minus:
                        return new Minus(atom);
                    case ExpressionTokenType.Not:
                        return new Not(atom);    
                }
            }
            return Atom();
        }

        private IExpression Atom()
        {
            IExpression atom;

            switch (lookAhead.Type)
            {
                case ExpressionTokenType.Identifier:
                    return Identifier();
                case ExpressionTokenType.LeftParen:
 			        NextToken();
			        atom = Expression();
			        if (lookAhead.Type != ExpressionTokenType.RightParen) {
				        throw UnexpectedTokenException("Expected ')'.");	
			        }
			        atom = new ParenthesisExpression(atom);
                    break;
                case ExpressionTokenType.Text:
                    var text = lookAhead.Sequence.Substring(1, lookAhead.Sequence.Length - 2);
                    atom = new Text(text);
                    break;
                case ExpressionTokenType.Integer:
                    var integer = long.Parse(lookAhead.Sequence, CultureInfo.InvariantCulture);
                    atom = new Integer(integer);
                    break;
                case ExpressionTokenType.Decimal:
                    var number = double.Parse(lookAhead.Sequence, CultureInfo.InvariantCulture);
			        atom = new Number(number);
                    break;
                case ExpressionTokenType.True:
                case ExpressionTokenType.False:
                    var boolean = lookAhead.Sequence.ToLowerInvariant() == "true";
			        atom = new Boolean(boolean);
                    break;
                case ExpressionTokenType.RegexPattern:
                    var pattern = lookAhead.Sequence.Substring(1, lookAhead.Sequence.Length - 2);
                    atom = new RegExp(pattern);
                    break;
                default:
                    throw UnexpectedTokenException();	
            }

            NextToken();
            return atom;
        }

        private IExpression Identifier()
        {
		    var identifier = lookAhead.Sequence;
		    NextToken();
		    if (lookAhead.Type == ExpressionTokenType.LeftParen) {
			    NextToken();
			    return FunctionCall(identifier);
		    }
            return new VariableExpression(identifier);
	    }

        private IExpression FunctionCall(string functionName)
	    {
            if (!Functions.ContainsKey(functionName))
            {
                throw new FormatException("Unknown function: '" + functionName + "'");
            }
            var function = (IFunctionExpression)Activator.CreateInstance(Functions[functionName]);

            // Parse the expected number of arguments to the function
	        for (int i = 1; i <= function.ArgumentCount; i++)
	        {
	            var argument = FunctionCallArgument(i == function.ArgumentCount);
                function.AddArgument(argument);
	        }

		    if (lookAhead.Type != ExpressionTokenType.RightParen) {
                throw UnexpectedTokenException("Expected ')'.");
			}
            NextToken();

            return function;
	    }

        private IExpression FunctionCallArgument(bool isLastArgument)
        {
            IExpression argument;
            if (lookAhead.Type == ExpressionTokenType.RegexPattern)
            {
                var pattern = lookAhead.Sequence.Substring(1, lookAhead.Sequence.Length - 2);
                argument = new RegExp(pattern);
                NextToken();
            }
            else
            {
                argument = Expression();
            }
            if (!isLastArgument)
            {
                if (lookAhead.Type == ExpressionTokenType.Comma)
                {
                    NextToken();
                }
                else
                {
                    throw UnexpectedTokenException("Expected ','.");
                }
		    } 
		    return argument;
	    }

        private void NextToken()
        {
            if (priorityTokenQueue.Count > 0)
            {
                lookAhead = priorityTokenQueue.Dequeue();
            }
            else
            {
                lookAhead = tokens.MoveNext() ? tokens.Current : ExpressionToken.Epsilon;
            }
        }

        #region Error/exception handling

        private FormatException UnexpectedTokenException(string expectedMessage = "")
        {
            string message = "Pos " + lookAhead.Position + ": ";
            if (lookAhead == ExpressionToken.Epsilon)
                message += "Unexpected end of expression. " + expectedMessage;
            else
                message += "Unexpected token: '" + lookAhead.Sequence + "'. " + expectedMessage;

             
            return new FormatException(message); 
        }

        #endregion
    }
}