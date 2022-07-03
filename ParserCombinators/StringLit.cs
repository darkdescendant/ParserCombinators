
namespace ParserCombinators
{
    public class StringLit : Parser
    {

        Sequence matchParser;

        private void BuildParserForString(string toMatch)
        {
            List<Parser> litParsers = new List<Parser>();

            foreach (char c in toMatch)
            {
                litParsers.Add(new Lit(c));
            }

            matchParser = new Sequence(litParsers);
        }

        public StringLit(string toMatch)
        {
            BuildParserForString(toMatch);
        }

        public StringLit(string toMatch, Func<ParserState, ParserState> f = null) : base(f)
        {
            BuildParserForString(toMatch);
        }

        protected override ParserState ParseFunc()
        {
            var newParserState = matchParser.Apply(this.parserState);
            if (newParserState.isError)
                return newParserState;

            string matchString = "";
            var lps = (newParserState as ParserState<List<ParserState>>)?.data;
            if (lps == null)
                return newParserState.ToError("StringLit: Failed to convert matched parser state to string");

            foreach (var ps in lps)
            {
                var cps = ps as ParserState<char>;
                if (cps != null)
                    matchString += cps.data;
            }

            return ParserState<string>.WithData(newParserState, newParserState.index, matchString);
        }
    }
}
