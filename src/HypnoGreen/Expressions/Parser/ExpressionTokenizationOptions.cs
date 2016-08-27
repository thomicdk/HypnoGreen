using System;

namespace HypnoGreen.Expressions.Parser
{
    [Flags]
    internal enum ExpressionTokenizationOptions
    {
        None = 0,

        /// <summary>Will force tokenization of literals that would otherwise be 
        /// interpreted as a regex pattern.
        /// E.g. /100/ will be three tokens DIV INTEGER DIV.
        /// </summary>
        ExcludeRegex = 1
    }
}