﻿namespace HypnoGreen.Expressions.Parser
{
    internal enum ExpressionTokenType
    {
        Epsilon,
        Comma,
        LeftParen,
        RightParen,
        Identifier,
        Text,
        RegexPattern,
        Integer,
        Decimal,
        True,
        False,
        Not,
        Multiplication,
        Division,
        Remainder,
        Plus,
        Minus,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Equal,
        NotEqual,
        And,
        Or
    }
}