using TextParser.Logic.Interfaces;

namespace TextParser.Infrastructure;

public class FileManager :IFileManager
{
    private const string AssetDirectory = "./assets";
    private const string KeywordsFile = $"{AssetDirectory}/keywords.dict";
    private const string FontFile = $"{AssetDirectory}/font.dict";
    
    public string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("Wanted File Does not exist");
    
        return File.ReadAllText(path);
    }
    
    public IEnumerable<string> ReadFileLines(string path) => 
        !File.Exists(path) ? Enumerable.Empty<string>() : File.ReadLines(path);

    public IEnumerable<string> ReadFontLines() 
        => ReadFileLines(FontFile);

    public IEnumerable<string> ReadKeywordLines()
        => ReadFileLines(KeywordsFile);
}