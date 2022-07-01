using System.Text.RegularExpressions;

namespace ParserCombinators
{
    public class IsChar : Parser
    {
        public IsChar() : base()
        {
        }

        public IsChar(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        protected override ParserState ParseFunc()
        {
            char c = (char)parserState.stream.ReadByte();
            if (!Regex.Match(c.ToString(), @"[a-zA-Z]").Success)
                return parserState.ToError("");


            return ParserState<char>.WithData(parserState, parserState.index + 1, c);
        }

    }

}
