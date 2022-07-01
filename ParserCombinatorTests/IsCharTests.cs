using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    public class IsCharTests
    {
        ParserState ps;

        [SetUp]
        public void Setup()
        {
            ps = new ParserState();
            ps.index = 0;
            ps.isError = false;
        }

        [Test]
        public void IsCharMatches()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("H")))
            {
                ps.stream = ms;
                var parser = new IsChar();
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.AreEqual('H', (rps as ParserState<char>).data);
            }
        }

        [Test]
        public void IsCharHandlesEmpty()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("")))
            {
                ps.stream = ms;
                var parser = new IsChar();
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
            }
        }

        [Test]
        public void IsCharHandlesNonChar()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("\n")))
            {
                ps.stream = ms;
                var parser = new IsChar();
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
            }
        }
    }
}