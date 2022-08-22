using TextParser.Models.Constants;
using TextParser.Models.Data;
using TextParser.Models.Enums;

namespace TextParser.Logic;

public record AnalyzeTextRequest(IEnumerable<TextChunk> Chunks);

public class AnalyzeTextHandler
{
    public static IEnumerable<TextProcessingDetails> Do(AnalyzeTextRequest request)
    {
        var processingDetails = new List<TextProcessingDetails>();
        foreach (var chunk in request.Chunks)
        {
            var flag = chunk.Text
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();
            
            var operation = ParseOperation.None;

            if (chunk.Parse && flag is not null)
            {
                // finding the elements to disable 
                var disableKeywords = flag.Contains(Symbols.DisableKeywordConversion);
                var disableFonts = flag.Contains(Symbols.DisableFontConversion);

                // checking the operations the code can handle
                operation = flag switch
                {
                    _ when  disableKeywords && disableFonts
                        => ParseOperation.None,
                    _ when disableKeywords
                        => ParseOperation.Font,
                    _ when disableFonts
                        => ParseOperation.KeyWords,
                    _ => ParseOperation.All
                };

                // checking the length of the used flags
                var flagsLength = operation switch
                {
                    ParseOperation.KeyWords =>
                        Symbols.DisableFontConversion.Length,
                    
                    ParseOperation.Font =>
                        Symbols.DisableKeywordConversion.Length,
                    
                    ParseOperation.None =>
                        Symbols.DisableKeywordConversion.Length
                        + Symbols.DisableFontConversion.Length,
                    
                    _ => 0
                };
                
                // removing all excess in the text
                if (flagsLength != 0 && chunk.Text.Length >= flagsLength)
                {
                    chunk.Text = chunk.Text[flagsLength..];
                }

                //Removing unneeded white space
                chunk.Text = chunk.Text.Trim();
            }

            // Details to add on shit
            processingDetails.Add(new TextProcessingDetails
            {
                Text = chunk.Text,
                Operation = operation,
            });
        }
        
        return processingDetails;
    }
}