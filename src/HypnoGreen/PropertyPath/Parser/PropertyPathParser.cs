using System;
using System.Collections.Generic;

namespace HypnoGreen.PropertyPath.Parser
{
    internal class PropertyPathParser
    {
        private IEnumerator<PropertyPathToken> _tokens;

        private PropertyPathToken _lookAhead;

        public PropertyPath Parse(string path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path == "") // Special case
                return new PropertyPath(new Property(""));

            var tokenizer = new PropertyPathTokenizer();
            _tokens = tokenizer.Tokenize(path).GetEnumerator();
            NextToken();

            var parsedPath = Path();

            if (_lookAhead != PropertyPathToken.Epsilon)
                throw UnexpectedTokenException("Expected [Epsilon].");

            return parsedPath;
        }

        private PropertyPath Path()
        {
            var property = Property();
            if (_lookAhead.Type == PropertyPathTokenType.Dot)
            {
                NextToken();
                return new PropertyPath(property, Path());
            }
            return new PropertyPath(property);
        }

        private Property Property()
        {
            if (_lookAhead.Type != PropertyPathTokenType.Identifier)
            {
                throw UnexpectedTokenException("Expected a property name.");
            }

            var property = _lookAhead.Sequence;
            NextToken();
            return ArrayIndex(property); 
        }

        private Property ArrayIndex(string property)
        {
            if (_lookAhead.Type == PropertyPathTokenType.LeftSquareBracket)
            {
                NextToken();
                if (_lookAhead.Type != PropertyPathTokenType.ArrayIndex)
                {
                    throw UnexpectedTokenException("Expected array index.");
                }
                var arrayIndex = int.Parse(_lookAhead.Sequence);
                NextToken();
                if (_lookAhead.Type != PropertyPathTokenType.RightSquareBracket)
                {
                    throw UnexpectedTokenException("Expected ']'.");
                }
                NextToken();
                return new ArrayItemProperty(property, arrayIndex);
            }
            return new Property(property);
        }


        private FormatException UnexpectedTokenException(string expectedMessage)
        {
            var message = $"Unexpected token: '${_lookAhead.Sequence}'. ${expectedMessage}";
            return new FormatException(message); 
        }

        private void NextToken()
        {
            _lookAhead = _tokens.MoveNext() ? _tokens.Current : PropertyPathToken.Epsilon;
        }
    }
}
