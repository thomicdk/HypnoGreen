using System;
using System.Collections.Generic;
using HypnoGreen.Expressions;
using HypnoGreen.Expressions.Additive;
using HypnoGreen.Expressions.Conditional;
using HypnoGreen.Expressions.Equality;
using HypnoGreen.Expressions.Function;
using HypnoGreen.Expressions.Function.Text;
using HypnoGreen.Expressions.Multiplicative;
using HypnoGreen.Expressions.Parser;
using HypnoGreen.Expressions.Relational;
using HypnoGreen.Expressions.Unary;
using HypnoGreen.Types;
using NUnit.Framework;
using Boolean = HypnoGreen.Types.Boolean;
using Contains = HypnoGreen.Expressions.Function.Text.Contains;

namespace HypnoGreen.Test.Expressions.Parser
{
    [TestFixture]
    public class ExpressionParserTest
    {
        private static IEnumerable<object[]> ExpressionTestCases()
        {
            return new List<object[]> 
                {
                    // { EXPRESSION, EXPECTED_RESULT, EXPECTED_TYPE }
                    new object[] {"true", true, typeof(Boolean) },
                    new object[] {"4", 4, typeof(Integer) },
                    new object[] {"3.14", 3.14, typeof(Number) },
                    new object[] {"/[A-z]+/", "/[A-z]+/", typeof(RegExp) },
                    new object[] {"\"Michael\"", "Michael", typeof(Text) },
                    new object[] {"5 == 2", false, typeof(Equals) },
                    new object[] {"5 != 2", true, typeof(NotEquals) },
                    new object[] {"Contains(Person.Name, \"Michael\")", true, typeof(Contains) },
                    new object[] {"EndsWith(Person.Name, \"Michael\")", true, typeof(EndsWith) },
                    new object[] {"Matches(Person.Name, /[A-Z]+/)", true, typeof(Matches) },
                    new object[] {"StartsWith(Person.Name, \"Michael\")", true, typeof(StartsWith) },
                    new object[] {"IsEmpty(Person.Name)", false, typeof(IsEmpty)  },
                    new object[] {"NotEmpty(Person.Name)", true, typeof(NotEmpty)  },
                    new object[] {"true && true", true, typeof(And)  },
                    new object[] {"true AND true", true, typeof(And)  },
                    new object[] {"true || false", true, typeof(Or)  },
                    new object[] {"true OR false", true, typeof(Or)  },
                    new object[] {"!(true)", false, typeof(Not)  },
                    new object[] {"+35", 35, typeof(Plus)  },
                    new object[] {"-83", -83, typeof(Minus)  },
                    new object[] {"4*2", 8, typeof(Multiplication)  },
                    new object[] {"42/7", 6, typeof(Division)  },
                    new object[] {"4%3", 1, typeof(Remainder)  },
                    new object[] {"4+2", 6, typeof(Addition)  },
                    new object[] {"9-2", 7, typeof(Subtraction)  },
                    new object[] {"Person.Name", "Michael", typeof(VariableExpression)  },
                    new object[] {"5 > 18", false, typeof(GreaterThan)  },
                    new object[] {"5 >= 18", false, typeof(GreaterThanOrEquals)  },
                    new object[] {"5 < 18", true, typeof(LessThan)  },
                    new object[] {"5 <= 18", true, typeof(LessThanOrEquals)  },
                    new object[] {"(4+3)", 7, typeof(ParenthesisExpression)  },
                };
        }

        [Test, TestCaseSource(nameof(ExpressionTestCases))]
        public void Parse_ExpressionType_IsParsedCorrect(string input, object expectedResult, Type expectedType)
        {
            var parser = new ExpressionParser();
            var expression = parser.Parse(input);
            Assert.IsInstanceOf(expectedType, expression);
        }

        [Test, TestCaseSource(nameof(ExpressionTestCases))]
        public void Parse_ExpressionEvaluation_ReturnsExpectedResult(string input, object expectedResult, Type expectedType)
        {
            var parser = new ExpressionParser();
            var expression = parser.Parse(input);
            var result = expression.EvaluateWithData(new { Person = new { Name = "Michael" } });
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase("5+5,")]
        [TestCase("&3*2")]
        [TestCase("()*2")]
        [TestCase("(4+1")]
        [TestCase("UnknownFunction(4)")]
        [TestCase("IsEmpty(4")]
        [TestCase("Contains(4)")]
        public void Parse_InvalidExpression_ThrowsFormatException(string expression)
        {
            var parser = new ExpressionParser();
            Assert.Throws<FormatException>(() =>
            {
                parser.Parse(expression);
            });
        }

        [Test]
        public void Parse_PathExpression_IsParsedCorrect()
        {
            var data = new
            {
                Person = new[]
                {
                    new { Age = 6 },
                    new { Age = 3 },
                    new { Age = 17 }
                },
                TotalAge = 26
            };
            var parser = new ExpressionParser();
            var expression = parser.Parse("Person[0].Age + Person[1].Age + Person[2].Age == TotalAge");
            var value = expression.EvaluateWithData<bool>(data);
            Assert.True(value);
        }
    }
}
