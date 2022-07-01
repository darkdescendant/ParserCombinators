using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCombinators
{
    public class Many : Parser
    {
        Parser parserToMatch;

        public Many(Parser p)
        {
            parserToMatch = p;
        }

        public Many(Parser p, Func<ParserState, ParserState> f = null) : base(f)
        {
            parserToMatch = p;
        }

        protected override ParserState ParseFunc()
        {
            List<ParserState> results = new List<ParserState>();

            var newState = parserState;
            while (true)
            {
                var lastState = newState;
                newState = parserToMatch.Apply(lastState);
                if (newState.isError)
                {
                    lastState.stream.Seek(lastState.index, SeekOrigin.Begin);
                    break;
                }

                results.Add(newState);
            }

            return ParserState<List<ParserState>>.WithData(parserState, newState.index, results);
        }
    }
}
