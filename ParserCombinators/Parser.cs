namespace ParserCombinators
{
    public class ParserState
    {
        public int index = 0;
        public Stream stream = null;
        public bool isError = false;
        public string errorMessage = "";

        public ParserState ToError(string error)
        {
            this.stream.Seek(index, SeekOrigin.Begin);

            return new ParserState
            {
                index = this.index,
                stream = this.stream,
                isError = true,
                errorMessage = error
            };
        }
    }

    public class ParserState<T> : ParserState
    {
        public T data = default;

        public static ParserState WithData<U>(ParserState state, int newIndex, U d)
        {
            return new ParserState<U>
            {
                index = newIndex,
                stream = state.stream,
                data = d
            };
        }
    }

    public class Parser
    {
        static string EOI(string name) => $"{name}: End of Input";

        public ParserState parserState;
        public Func<ParserState, ParserState> parsedHandler = ps => ps;

        public Parser()
        {
            this.parserState = new ParserState();
            this.parsedHandler = parserState => parserState;
        }


        public Parser(Func<ParserState, ParserState> f = null)
        {
            this.parserState = new ParserState();
            this.parsedHandler = f ?? this.parsedHandler;
        }

        protected virtual ParserState ParseFunc()
        {
            return this.parserState;
        }

        public ParserState Apply(ParserState parserState)
        {
            if (parserState.stream == null)
            {
                throw new InvalidOperationException("Null input stream");
            }

            if (parserState.isError)
                return parserState;

            if (parserState.stream.Position >= parserState.stream.Length)
            {
                return parserState.ToError(this.GetType().Name);
            }

            this.parserState = parserState;

            var newParseState = ParseFunc();
            if (newParseState.isError)
                return newParseState;

            return parsedHandler(newParseState);
        }

    }

}
