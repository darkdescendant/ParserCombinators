using ParserCombinators;
using System.Text;

namespace ParserApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ms = new MemoryStream(Encoding.ASCII.GetBytes("H1ello, World")))
            {
                ParserState ps = new ParserState();
                ps.stream = ms;
                ps.index = 0;
                ps.isError = false;

                var parser = new Choice(
                    new Sequence(new IsNum(), new IsNum()),
                    new Sequence(new IsChar(), new IsNum()));

                parser.parserState = ps;
                var parserState = parser.Apply(ps);

                if (parserState.isError)
                {
                    Console.WriteLine(parserState.errorMessage);
                }

                var litParser = new Lit('H');
                ps.stream.Seek(0, SeekOrigin.Begin);
                ps.index = 0;
                parserState = litParser.Apply(ps);

                if (parserState.isError)
                {
                    Console.WriteLine(parserState.errorMessage);
                }
                else
                {
                    Console.WriteLine($"Found literal {(parserState as ParserState<char>)?.data} at index {parserState.index}");
                }
            }
        }
    }
}
