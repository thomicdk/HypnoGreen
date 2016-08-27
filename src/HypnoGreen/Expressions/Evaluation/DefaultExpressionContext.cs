using System;
using System.Globalization;
using HypnoGreen.PropertyPath;
using HypnoGreen.PropertyPath.Parser;

namespace HypnoGreen.Expressions.Evaluation
{
    public class DefaultExpressionContext : ExpressionContext
    {
        private readonly object _data;

        public DefaultExpressionContext()
            : this(null, CultureInfo.InvariantCulture, true)
        { }

        public DefaultExpressionContext(object data)
            : this(data, CultureInfo.InvariantCulture, true)
        { }

        public DefaultExpressionContext(object data, CultureInfo cultureInfo, bool ignoreCase)
            : base(cultureInfo, ignoreCase)
        {
            _data = data;
        }

        public override object ResolveVariable(string variable)
        {
            var path = new PropertyPathParser().Parse(variable);
            return path.ResolveValue(_data, new ReflectionPropertyValueResolver());
        }

        public override bool HasVariable(string variable)
        {
            try
            {
                ResolveVariable(variable);
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}