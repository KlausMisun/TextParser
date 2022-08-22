using System.Text;

namespace TextParser.Logic;

public record KeywordTransformRequest(string Text, Dictionary<string, string> Keywords);

public class KeywordTransformHandler
{
    public static string Do(KeywordTransformRequest request)
    {
        var sb = new StringBuilder(request.Text);

        foreach (var key in request.Keywords.Keys)
            sb.Replace(key, request.Keywords[key]);
        
        return sb.ToString();
    }
}