namespace ParserCombinators
{
    public class Choice : Parsers
    {
        public Choice()
        {
        }

        public Choice(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        public Choice(List<Parsers> parsers) : base(parsers)
        {
        }

        public Choice(Parser p0, Func<ParserState, ParserState> f = null) : base(p0, f)
        {
        }

        public Choice(List<Parsers> parsers, Func<ParserState, ParserState> f = null) : base(parsers, f)
        {
        }

        public Choice(Parser p0, Parser p1, Func<ParserState, ParserState> f = null) : base(p0, p1, f)
        {
        }

        public Choice(Parser p0, Parser p1, Parser p2, Func<ParserState, ParserState> f = null) : base(p0, p1, p2, f)
        {
        }

        protected override ParserState ParseFunc()
        {
            if (this.parserState.isError)
                return this.parserState;

            var startIndex = parserState.index;
            var newState = this.parserState;

            foreach (var p in parsers)
            {
                newState = p.Apply(this.parserState);
                if (!newState.isError)
                    return newState;

                parserState.stream.Seek(startIndex, SeekOrigin.Begin);
            }

            return this.parserState.ToError("Unable to match any parser in parser list");
        }
    }

}
