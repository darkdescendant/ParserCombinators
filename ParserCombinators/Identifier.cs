
namespace ParserCombinators
{
    public class Identifier : Parser
    {
        Sequence matchParser = new Sequence(
           new Choice(new IsChar(), new Lit('_')),
           new Many(new Choice(new IsChar(), new IsNum(), new Lit('_'))));

        public Identifier()
        {
        }

        public Identifier(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        string CollapseParserState(ParserState<char> ps)
        {
            return ps.data.ToString();
        }

        string CollapseParserState(List<ParserState> lps)
        {
            var ret = "";
            foreach (var ps in lps)
            {
                if (ps is ParserState<char>)
                {
                    ret += CollapseParserState(ps as ParserState<char>);
                }
                else if (ps is ParserState<List<ParserState>>)
                {
                    ret += CollapseParserState((ps as ParserState<List<ParserState>>)?.data);
                }
                else
                {
                    throw new InvalidOperationException("Unknown parser state collapsing identifier");
                }
            }

            return ret;
        }

        protected override ParserState ParseFunc()
        {
            var currIndex = this.parserState.index;
            var newParserState = matchParser.Apply(this.parserState);
            if (newParserState.isError)
            {
                return this.parserState.ToError("Identifier: No match");
            }

            var lps = newParserState as ParserState<List<ParserState>>;
            if (lps == null)
                return this.parserState.ToError("Identifier: Matched but unexpeced return");

            var ident = CollapseParserState(lps.data);

            return ParserState<string>.WithData(newParserState, newParserState.index, ident);
        }
    }
}
