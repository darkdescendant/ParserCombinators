using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    internal class ParserTests : ParserTestsBase
    {
        [Test]
        public void ParserStateConvert()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("[")))
            {
                ps.stream = ms;
                var parser = new Lit('[', parserState =>
                {
                    if (parserState.isError)
                        return parserState;

                    if ((parserState as ParserState<char>).data == '[')
                    {
                        return ParserState<string>.WithData(parserState, parserState.index, "FOUND BRACE");
                    }

                    return parserState.ToError("Faile converting Literal.");
                });
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.AreEqual("FOUND BRACE", (rps as ParserState<string>).data);
            }

        }

    }
}
