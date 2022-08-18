namespace TextParser.Models.Processors;

public class WordMatcher
{
    public string Word { get; }
    private readonly int[] _indexTable;
    private int SearchIndex { get; set; } = 0;
    private char CurrentLetter => Word[SearchIndex];
    
    public WordMatcher(string word)
    {
        if (string.IsNullOrEmpty(word)) throw new ArgumentNullException(nameof(word));
        Word = word;


        _indexTable = new int[word.Length];
        GenerateLookupTable(word);
    }
    
    private void GenerateLookupTable(string word)
    {
        _indexTable[0] = 0;

        var primaryIndex = 1;
        var routerIndex = 0;

        while (primaryIndex < word.Length)
        {

            // If the letters match we set the current router position to be the closest for our code to jump to
            if (word[primaryIndex] == word[routerIndex])
            {
                _indexTable[primaryIndex] = routerIndex;
                primaryIndex++;
                routerIndex++;
                continue;
            }

            
            if (routerIndex == 0)
            {
                _indexTable[primaryIndex] = routerIndex;
                primaryIndex++;
                continue;                
            }

            routerIndex = _indexTable[routerIndex - 1];
        }

    }

    public bool ContinuousMatch(char letter)
    {
        // Loop around the table until we reach the start of table then decide to return
        while (true)
        {
            // If the current letter matches target we increase the search index 
            if (letter == CurrentLetter)
            {
                SearchIndex++;

                // We check if the entire word has been checked if not then return false
                if (SearchIndex != Word.Length) return false;

                // Reset the matching process
                Reset();
                return true;
            }

            // If Search index is 0, stop matching and search
            if (SearchIndex == 0) return false;

            // Get the current letter
            SearchIndex = _indexTable[SearchIndex - 1];
        }
    }

    private void Reset()
    {
        SearchIndex = 0;
    }

    public bool FullMatch(string input)
    {
        // Reset state 
        Reset();
        
        // Matching all parts of the code
        return input.Any(ContinuousMatch);
    }
}