namespace HypnoGreen.Types
{
    /// <summary>Singleton</summary>
    internal class Null : ValueType<object>
    {
        private static Null _instance;
        public static Null Instance => _instance ?? (_instance = new Null());

        public override object Value => null;

        private Null () { }

        public override bool Equals(object obj)
        {
            return obj is Null;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}