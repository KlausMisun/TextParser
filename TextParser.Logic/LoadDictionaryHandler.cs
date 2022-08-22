namespace TextParser.Logic;

public record LoadDictionaryRequest(IEnumerable<string> Lines);

public static class LoadDictionaryHandler
{
    public static Dictionary<string, string> Do(LoadDictionaryRequest request)
    {
        Dictionary<string, string> dictionary = new();

        // check all dictionary lines
        foreach (var line in request.Lines)
        {
            // split by definition symbol
            var data = line.Split(':');

            // get the key and trim exess space
            var key = data[0].Trim();
            
            // get value and remove excess space
            var value = data[1].Trim();

            // If value is wrapped with "" we remove them and extract the inner value
            if (value.StartsWith('"') && value.EndsWith('"'))
                value = value[1..^1];

            // adding key and value to dictionary
            dictionary.Add(key, value);
        }

        // returning the dictionary
        return dictionary;
    }
}