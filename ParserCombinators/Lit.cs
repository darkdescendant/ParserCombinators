namespace ParserCombinators
{
    public class Lit : Parser
    {
        char toMatch;

        public Lit(char c)
        {
            this.toMatch = c;
        }

        public Lit(char c, Func<ParserState, ParserState> f = null) : base(f)
        {
            this.toMatch = c;
        }

        protected override ParserState ParseFunc()
        {
            char c = (char)parserState.stream.ReadByte();
            if (c != this.toMatch)
                return parserState.ToError($"Lit: Failed to match {this.toMatch}");

            return ParserState<char>.WithData(this.parserState, this.parserState.index + 1, c);
        }
    }

}
