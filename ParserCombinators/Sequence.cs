namespace ParserCombinators
{
    public class Sequence : Parsers
    {

        public Sequence()
        {
        }

        public Sequence(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        public Sequence(List<Parsers> parsers) : base(parsers)
        {
        }

        public Sequence(Parser p0, Func<ParserState, ParserState> f = null) : base(p0, f)
        {
        }

        public Sequence(List<Parsers> parsers, Func<ParserState, ParserState> f = null) : base(parsers, f)
        {
        }

        public Sequence(Parser p0, Parser p1, Func<ParserState, ParserState> f = null) : base(p0, p1, f)
        {
        }

        public Sequence(Parser p0, Parser p1, Parser p2, Func<ParserState, ParserState> f = null) : base(p0, p1, p2, f)
        {
        }

        protected override ParserState ParseFunc()
        {
            var startIndex = parserState.index;
            var newState = parserState;
            var parserStates = new List<ParserState>();

            foreach (var parser in parsers)
            {
                newState = parser.Apply(newState);
                if (newState.isError)
                {
                    return newState.ToError("Error matching expected sequence.");
                }
                parserStates.Add(newState);
            }

            return ParserState<List<ParserState>>.WithData(parserState, newState.index, parserStates);
        }
    }

}
