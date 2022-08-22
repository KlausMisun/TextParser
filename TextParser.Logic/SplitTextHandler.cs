using System.Text;
using TextParser.Models.Constants;
using TextParser.Models.Data;
using TextParser.Models.Processors;

namespace TextParser.Logic;

public record SplitTextRequest(string Input);

public static class SplitTextHandler
{
    public static IEnumerable<TextChunk> Do(SplitTextRequest request)
    {
        var sb = new StringBuilder();
        var sections = new List<TextChunk>();

        var specialText = false;
        var startMatcher = new WordMatcher(Symbols.StartParsingSymbol);
        var stopMatcher = new WordMatcher(Symbols.StopParsingSymbol);

        // Loop over letters and check for text
        foreach (var letter in request.Input)
        {
            // add letter to our buffer
            sb.Append(letter);
            
            // check if we have hit a keyword and we are ready for extraction
            var extract = specialText switch
            {
                false => startMatcher.ContinuousMatch(letter),
                true => stopMatcher.ContinuousMatch(letter),
            };

            // If a start keyword has been matched skip we extract the saved text
            if (!extract) continue;


            // remove brackets since we do not need them anymore
            if (specialText)
                sb.Remove(sb.Length - stopMatcher.Word.Length, stopMatcher.Word.Length);
            else
                sb.Remove(sb.Length - startMatcher.Word.Length, startMatcher.Word.Length);

            // add text to the list
            sections.Add(new TextChunk
            {
                Text = sb.ToString(),
                Parse = specialText,
            });
            
            // clear buffer
            sb.Clear();
            
            // invert the current status as we either enter or leave brackets
            specialText = !specialText;
        }

        // needed to extract any remaining text that hadn't hit a keyword towards the end of the input
        if (sb.Length != 0)
        {
            sections.Add(new TextChunk
            {
                Text = sb.ToString(),
                Parse = specialText,
            });
        }

        // Remove empty input
        for (var index = 0; index < sections.Count; index++)
        {
            if (string.IsNullOrEmpty(sections[index].Text))
            {
                sections.Remove(sections[index]);
            }
        }

        // returning the data
        return sections;
    }
    
}