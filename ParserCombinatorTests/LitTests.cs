using ParserCombinators;
using System.Text;
using System;

namespace ParserCombinatorTests
{
    internal class LitTests
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
        public void MatchesLit()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("[")))
            {
                ps.stream = ms;
                var parser = new Lit('[');
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.AreEqual('[', (rps as ParserState<char>).data);
            }

        }

        [Test]
        public void FailedToMatchLit()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("]")))
            {
                ps.stream = ms;
                var parser = new Lit('[');
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
                Assert.AreEqual("Lit: Failed to match [", rps.errorMessage);
            }

        }
    }

}
