using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    internal class ChoiceTests : ParserTestsBase
    {

        [Test]
        public void CanMatchFirstChoice()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("[[")))
            {
                ps.stream = ms;
                var parser = new Choice(
                   new Lit('['),
                   new Lit(']'));

                var parserState = parser.Apply(ps);
                Assert.IsFalse(parserState.isError);
                var lps = parserState as ParserState<char>;
                Assert.IsNotNull(lps);
                Assert.That(lps.data, Is.EqualTo('['));

            }
        }

        [Test]
        public void CanMatchSecondChoice()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("][")))
            {
                ps.stream = ms;
                var parser = new Choice(
                   new Lit('['),
                   new Lit(']'));

                var parserState = parser.Apply(ps);
                Assert.IsFalse(parserState.isError);
                var lps = parserState as ParserState<char>;
                Assert.IsNotNull(lps);
                Assert.That(lps.data, Is.EqualTo(']'));
            }
        }

        [Test]
        public void NoMatchFails()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("][")))
            {
                ps.stream = ms;
                var parser = new Choice(
                   new Lit('_'),
                   new Lit('['));

                var parserState = parser.Apply(ps);
                Assert.IsTrue(parserState.isError);
            }
 
        }
    }
}
