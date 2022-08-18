using System;
using System.Linq;
using TextParser.Models.Processors;
using Xunit;
using Xunit.Abstractions;

namespace TextParser.Models.Tests;

public class WordMatcherTests
{
    private readonly ITestOutputHelper _helper;

    public WordMatcherTests(ITestOutputHelper helper)
    {
        _helper = helper;
    }

    private bool Match(string word, string input) =>
        new WordMatcher(word).FullMatch(input);
    
    [Theory]
    [InlineData("Candy", "The kid stole the candy from the shop", false)]
    [InlineData("Candy", "The kid stole the Candy from the shop", true)]
    [InlineData("Melon", "This year Melons have been expensive", true)]
    [InlineData("Water", "Water is a rare drink, or not", true)]
    [InlineData("Wattttaaeeeer", "Water is a rare Wattttaaeeeer drink, or not", true)]
    [InlineData("WatWatWataaeeeer", "Water is a rare WatWatWataaeWatWatWataaeeeer drink, or not", true)]
    [InlineData("WatWatWataaeeeer", "Water is a rare WatxxxWatWatsaadcasdaaeWatWatWataaeeeer drink, or not", true)]
    [InlineData("WatWaetWataaeeeer", "Water is a rare WatxxxWatWatsaadcasdaaeWatWatWataaeeeer drink, or not", false)]
    [InlineData("Sake", "You bought sake today", false)]
    [InlineData("", "You bought sake today", false)]
    public void MatchWords(string? word, string input, bool expected)
    {
        if (string.IsNullOrEmpty(word))
            Assert.Throws<ArgumentNullException>(() => Match(word, input));
        else
            Assert.Equal(expected, Match(word, input));
    }
}