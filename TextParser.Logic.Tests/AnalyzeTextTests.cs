using System.Linq;
using TextParser.Models.Constants;
using TextParser.Models.Data;
using TextParser.Models.Enums;
using Xunit;

namespace TextParser.Logic.Tests;

public class AnalyzeTextTests
{
    [Fact]
    public void AnalyzeChunksOfTextCheckProperOperation()
    {
        var textChunks = new TextChunk[]
        {
            new TextChunk()
            {
                Text = "HELLO",
                Parse = true,
            },
            new TextChunk()
            {
                Text = "HELLO",
                Parse = false,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableFontConversion}GUARDIAN",
                Parse = true,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableKeywordConversion}GUARDIAN",
                Parse = true,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableFontConversion}{Symbols.DisableKeywordConversion}GUARDIAN",
                Parse = true,
            },
        };

        var expectedDetails = new TextProcessingDetails[]
        {
            new TextProcessingDetails
            {
                Text = "HELLO",
                Operation = ParseOperation.All
            },
            new TextProcessingDetails
            {
                Text = "HELLO",
                Operation = ParseOperation.None
            },
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.KeyWords,
            },
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.Font,
            },
            
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.None,
            },
        };

        var resultDetails = 
            AnalyzeTextHandler
                .Do(new AnalyzeTextRequest(textChunks))
                .ToArray();
        
        for (var i = 0; i < textChunks.Length; i++)
        {
            Assert.Equal(expectedDetails[i].Operation, resultDetails[i].Operation);
        }
    }
    
    [Fact]
    public void AnalyzeChunksOfTextCheckProperText()
    {
        var textChunks = new TextChunk[]
        {
            new TextChunk()
            {
                Text = "HELLO",
                Parse = true,
            },
            new TextChunk()
            {
                Text = "HELLO",
                Parse = false,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableFontConversion} GUARDIAN ",
                Parse = true,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableKeywordConversion}   GUARDIAN",
                Parse = true,
            },
            new TextChunk()
            {
                Text = $"{Symbols.DisableFontConversion}{Symbols.DisableKeywordConversion} GUARDIAN",
                Parse = true,
            },
        };

        var expectedDetails = new TextProcessingDetails[]
        {
            new TextProcessingDetails
            {
                Text = "HELLO",
                Operation = ParseOperation.All
            },
            new TextProcessingDetails
            {
                Text = "HELLO",
                Operation = ParseOperation.None
            },
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.KeyWords,
            },
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.Font,
            },
            
            new TextProcessingDetails()
            {
                Text = $"GUARDIAN",
                Operation = ParseOperation.None,
            },
        };

        var resultDetails = 
            AnalyzeTextHandler
                .Do(new AnalyzeTextRequest(textChunks))
                .ToArray();
        
        for (var i = 0; i < textChunks.Length; i++)
        {
            Assert.Equal(expectedDetails[i].Text, resultDetails[i].Text);
        }
    }
}