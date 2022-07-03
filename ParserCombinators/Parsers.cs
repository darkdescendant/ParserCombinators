
namespace ParserCombinators
{
    public abstract class Parsers : Parser
    {
        protected List<Parser> parsers = new List<Parser>();
        public Parsers()
        {
        }

        public Parsers(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        public Parsers(Parser p0, Func<ParserState, ParserState> f = null) : base(f)
        {
            parsers.Add(p0);
        }

        public Parsers(Parser p0, Parser p1, Func<ParserState, ParserState> f = null) : base(f)
        {
            parsers.Add(p0);
            parsers.Add(p1);
        }

        public Parsers(Parser p0, Parser p1, Parser p2, Func<ParserState, ParserState> f = null) : base(f)
        {
            parsers.Add(p0);
            parsers.Add(p1);
            parsers.Add(p2);
        }

        public Parsers(List<Parser> parsers)
        {
            this.parsers.AddRange(parsers);
        }


        public Parsers(List<Parser> parsers, Func<ParserState, ParserState> f = null) : base(f)
        {
            this.parsers.AddRange(parsers);
        }

    }
}
