using System.Text;

using ParserCombinators;

namespace ParserCombinatorTests
{
    internal class ConsumeWhitespaceTests : ParserTestsBase
    {
        [Test]
        public void CanConsumeWhitespace()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(" \t\n\r")))
            {
                ps.stream = ms;
                var parser = new ConsumeWhitespace();
                var parserState = parser.Apply(ps);
                Assert.IsFalse(parserState.isError);
                Assert.That(parserState.index, Is.EqualTo(4));
            }
        }

        [Test]
        public void CanConsumeNoWhitespace()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("AAAAAA")))
            {
                ps.stream = ms;
                var parser = new ConsumeWhitespace();
                var parserState = parser.Apply(ps);
                Assert.IsFalse(parserState.isError);
                Assert.That(parserState.index, Is.EqualTo(0));
            }
        }
    }
}
