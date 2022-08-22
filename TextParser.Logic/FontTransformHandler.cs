using System.Text;

namespace TextParser.Logic;

public record FontTransformRequest(string Text, Dictionary<string, string> Font);

public class FontTransformHandler
{
    public static string Do(FontTransformRequest request)
    {
        var sb = new StringBuilder(request.Text);
        foreach (var letter in request.Font.Keys)
            sb.Replace(letter, request.Font[letter]);
        
        return sb.ToString();
    }
}