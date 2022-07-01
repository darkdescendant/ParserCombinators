using ParserCombinators;
using System.Text;

namespace ParserCombinatorTests
{
    public class IsNumTests
    {
        ParserState ps;
        static char[] s_Nums = new char[]
        {
            '0',
            '1',
            '2',
            '3',
            '4',
            '5',
            '6',
            '7',
            '8',
            '9'
        };

        [SetUp]
        public void Setup()
        {
            ps = new ParserState();
            ps.index = 0;
            ps.isError = false;
        }


        [Test]
        public void IsNumMatches([ValueSource("s_Nums")]char num)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(num.ToString())))
            {
                ps.stream = ms;
                var parser = new IsNum();
                var rps = parser.Apply(ps);
                Assert.IsFalse(rps.isError);
                Assert.AreEqual(num, (rps as ParserState<char>).data);
            }
        }

        [Test]
        public void IsNumHandlesEmpty()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("")))
            {
                ps.stream = ms;
                var parser = new IsNum();
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
            }
        }

        [Test]
        public void IsNumHandlesNonChar()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("\n")))
            {
                ps.stream = ms;
                var parser = new IsNum();
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
            }
        }

        [Test]
        public void IsNumHandlesNonNumChar()
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes("A")))
            {
                ps.stream = ms;
                var parser = new IsNum();
                var rps = parser.Apply(ps);
                Assert.IsTrue(rps.isError);
            }
        }
    }
}