using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    internal class IdentifierTests : ParserTestsBase
    {
        [Test]
        public void MatchStringLiteral()
        {
            string ident = "thisIs_An_1dentifi3er";
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ident)))
            {
                ps.stream = ms;
                var parser = new Identifier();
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.That((rps as ParserState<string>)?.data, Is.EqualTo(ident));
            }

        }

        [Test]
        public void MatchStringLiteralLeadingUnderscore()
        {
            string ident = "_thisIs_An_1dentifi3er";
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ident)))
            {
                ps.stream = ms;
                var parser = new Identifier();
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.That((rps as ParserState<string>)?.data, Is.EqualTo(ident));
            }

        }

        [Test]
        public void MatchStringLiteralWithTrailingCharacters()
        {
            string ident = "_thisIs_An_1dentifi3er";
            string testIden = $"{ident}:_";
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(testIden)))
            {
                ps.stream = ms;
                var parser = new Identifier();
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.That((rps as ParserState<string>)?.data, Is.EqualTo(ident));
            }

        }

    }
}
