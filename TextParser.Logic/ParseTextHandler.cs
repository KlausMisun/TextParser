using System.Text;
using TextParser.Logic.Interfaces;
using TextParser.Models.Enums;

namespace TextParser.Logic;

public record ParseTextRequest(string Input);

public class ParseTextHandler
{
    private readonly IFileManager _manager;

    public ParseTextHandler(IFileManager manager)
    {
        _manager = manager;
    }
    
    public string Do(ParseTextRequest request)
    {
        var keywords = LoadDictionaryHandler
            .Do(new LoadDictionaryRequest(_manager.ReadKeywordLines()));

        var font = LoadDictionaryHandler
            .Do(new LoadDictionaryRequest(_manager.ReadFontLines()));
        
        var lines =SplitTextHandler.Do(new SplitTextRequest(request.Input));
        var details = AnalyzeTextHandler.Do(new AnalyzeTextRequest(lines));

        var sb = new StringBuilder();
        foreach (var detail in details)
        {
            var parsedLine = detail.Text;
            switch (detail.Operation)
            {
                case ParseOperation.KeyWords:
                    parsedLine = KeywordTransformHandler.Do(new KeywordTransformRequest(parsedLine, keywords));
                    break;
                case ParseOperation.Font:
                    parsedLine = FontTransformHandler.Do(new FontTransformRequest(parsedLine, font));
                    break;
                case ParseOperation.All:
                    parsedLine = KeywordTransformHandler.Do(new KeywordTransformRequest(parsedLine, keywords));
                    parsedLine = FontTransformHandler.Do(new FontTransformRequest(parsedLine, font));
                    break;
                case ParseOperation.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sb.Append(parsedLine);
        }

        return sb.ToString();
    }
}