using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    internal class SequenceTests : ParserTestsBase
    {
        static (List<Parser>, string, bool, int)[] s_MatchInfo = new (List<Parser>, string, bool, int)[]
        {
            (new List<Parser>(){new Lit('='), new Lit('_'), new Lit('{')}, "=_{", true, 3),
            (new List<Parser>(){new Lit('='), new Lit('-'), new Lit('{')}, "=_{", false, 0),
            (new List<Parser>(){new ConsumeWhitespace(), new IsNum(), new ConsumeWhitespace(), new IsChar()}, "    4y", true, 4),

        };

        [Test]
        public void CanMatchSequence([ValueSource("s_MatchInfo")](List<Parser>, string, bool, int) matchInfo)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(matchInfo.Item2)))
            {
                ps.stream = ms;
                var parser = new Sequence(matchInfo.Item1);
                var parserState = parser.Apply(ps);
                if (matchInfo.Item3)
                {
                    Assert.IsFalse(parserState.isError);
                    var lps = parserState as ParserState<List<ParserState>>;
                    Assert.IsNotNull(lps);
                    Assert.That(lps.data.Count, Is.EqualTo(matchInfo.Item4));

                    string value = "";
                    foreach(var ps in lps.data)
                    {
                        
                        value += (ps as ParserState<char>)?.data ?? '\0';
                    }

                    Console.WriteLine(value);
                }
                else
                {
                    Assert.IsTrue(parserState.isError);
                }
            }

        }

        [Test]
        public void CanNotMtchSequence()
        {

        }

    }
}
