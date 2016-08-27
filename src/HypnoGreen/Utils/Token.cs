namespace HypnoGreen.Utils
{
    internal class Token
    {
        private readonly string sequence;
        private readonly int position;

        public string Sequence
        {
            get { return sequence; }
        }

        public int Position
        {
            get { return position; }
        }

        public Token(string sequence, int position)
        {
            this.sequence = sequence;
            this.position = position;
        }
    }
}