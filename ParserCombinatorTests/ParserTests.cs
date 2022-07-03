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

        [Test]
        public void CanParseLabel()
        {
            var expectedIdent = "this_is_a_label";
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes($"    {expectedIdent}:")))
            {
                ps.stream = ms;
                var parser = new Sequence(new ConsumeWhitespace(),
                    new Identifier(),
                    new Lit(':'),
                    parserState =>
                {
                    if (parserState.isError)
                        return parserState;

                    var lps = (parserState as ParserState<List<ParserState>>);
                    if (lps == null || lps.data.Count < 3)
                        parserState.ToError("Invalid label found.");

                    return ParserState<string>.WithData(parserState, parserState.index, (lps.data[1] as ParserState<string>)?.data); 
                });

                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.That((rps as ParserState<string>)?.data, Is.EqualTo(expectedIdent));
            }

        }


    }
}
