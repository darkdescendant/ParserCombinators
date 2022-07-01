using ParserCombinators;

namespace ParserCombinatorTests
{
    internal class ParserTestsBase
    {
        protected ParserState ps;

        [SetUp]
        public void Setup()
        {
            ps = new ParserState();
            ps.index = 0;
            ps.isError = false;
        }
    }
}