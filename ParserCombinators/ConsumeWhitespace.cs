
using System.Text.RegularExpressions;

namespace ParserCombinators
{
    public class ConsumeWhitespace : Parser
    {
        public ConsumeWhitespace()
        {
        }

        public ConsumeWhitespace(Func<ParserState, ParserState> f = null) : base(f)
        {
        }

        protected override ParserState ParseFunc()
        {
            while (true)
            {
                char c = (char)parserState.stream.ReadByte();
                if (!Regex.Match(c.ToString(), @"\s").Success)
                    break;

                parserState.index += 1;
            }

            parserState.stream.Seek(-1, SeekOrigin.Current);
            return parserState;
        }
    }
}
