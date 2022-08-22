using System.Linq;
using Xunit;

namespace TextParser.Logic.Tests;

public class SplitTextTester
{
    [Theory]
    [InlineData("HELLO {{HEE}} ddd", "HELLO ", "HEE", " ddd")]
    [InlineData("HELLO ddd", "HELLO ddd")]
    [InlineData("HELLO {{ddd}}", "HELLO ", "ddd")]
    [InlineData("{{ddd}} HELLO", "ddd", " HELLO")]
    [InlineData("{{ ddd }} HELLO", " ddd ", " HELLO")]
    public void SplitTextIntoProperSections(string input, params string[] expected)
    {
        Assert.Equal(expected, SplitTextHandler.Do(new SplitTextRequest(input)).Select(x => x.Text).ToArray());
    }
    
    [Theory]
    [InlineData("HELLO {{HEE}} ddd", false, true, false)]
    [InlineData("HELLO ddd", false)]
    [InlineData("HELLO {{ddd}}", false, true)]
    [InlineData("{{ddd}} HELLO", true, false)]
    [InlineData("{{ ddd }} HELLO", true, false)]
    public void SplitTextAndCheckProperParsing(string input, params bool[] expected)
    {
        Assert.Equal(expected, SplitTextHandler.Do(new SplitTextRequest(input)).Select(x => x.Parse).ToArray());
    }
}