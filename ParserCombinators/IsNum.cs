using System.Text.RegularExpressions;

namespace ParserCombinators
{
    public class IsNum : Parser
    {
        public IsNum()
        {
        }

        public IsNum(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        protected override ParserState ParseFunc()
        {
            char c = (char)parserState.stream.ReadByte();
            if (!Regex.Match(c.ToString(), @"\d").Success)
                return parserState.ToError("");


            return ParserState<char>.WithData(parserState, parserState.index + 1, c);
        }
    }

}
