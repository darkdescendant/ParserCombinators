using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    internal class StringLiteralTests : ParserTestsBase
    {
        [Test]
        public void MatchStringLiteral()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("Test")))
            {
                ps.stream = ms;
                var parser = new StringLit("Test");
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.That((rps as ParserState<string>)?.data, Is.EqualTo("Test"));
            }
 
        }

        [Test]
        public void FailMatchStringLiteral()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("Rest")))
            {
                ps.stream = ms;
                var parser = new StringLit("Test");
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
                Assert.That(rps.index, Is.EqualTo(0));
            }
 
        }
     }
}
