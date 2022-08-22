using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using TextParser.Logic.Interfaces;
using Xunit;

namespace TextParser.Logic.Tests;

public class LoadDictionaryTests
{
    [Fact]
    public void DictionaryLoadedProperly()
    {
        const string file = "./TestData/Keywords.template";
        var resultDictionary = LoadDictionaryHandler.Do(new LoadDictionaryRequest(File.ReadLines(file)));
        
        var expectedDictionary = new Dictionary<string, string>
        {
            { "HELLO_WORD", "World Manipulation" },
            { "HELLO_HEALTH", "Health Manipulation" },
            { "HELLO_SPACE", "          " },
        };

        var expectedKeys = expectedDictionary.Keys.ToArray();
        
        foreach (var key in expectedKeys)
        {
            Assert.Equal(expectedDictionary[key], resultDictionary[key]);
        }
    }
}