using ParserCombinators;
using System.Text;

using ParserCombinators;

namespace ParserCombinatorTests
{
    internal class ManyTests : ParserTestsBase
    {
        static (string, bool, int)[] s_ManyMatches = new (string, bool, int)[] {
            ( "", true, 0 ),
            ( " ", false, 0 ),
            ( "[", false, 1 ),
            ( "[[", false, 2 ),
            ( "[[[", false, 3 ),
            ( "[ [", false, 1 ),
            ( " [", false, 0 )
        };

        [Test]
        public void CanMatchAllInstances([ValueSource("s_ManyMatches")](string, bool, int) matchInfo)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(matchInfo.Item1)))
            {
                ps.stream = ms;
                var parser = new Many(new Lit('['));
                var parserState = parser.Apply(ps);
                if (!matchInfo.Item2)
                {
                    Assert.IsFalse(parserState.isError);
                    var lps = parserState as ParserState<List<ParserState>>;
                    Assert.IsNotNull(lps);
                    Assert.That(lps.data.Count, Is.EqualTo(matchInfo.Item3));
                }
                else
                {
                    Assert.IsTrue(parserState.isError);
                }
            }

        }
    }
}
