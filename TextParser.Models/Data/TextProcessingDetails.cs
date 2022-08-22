using TextParser.Models.Enums;

namespace TextParser.Models.Data;

public class TextProcessingDetails
{
    public string Text { get; set; }
    public ParseOperation Operation { get; set; }
}