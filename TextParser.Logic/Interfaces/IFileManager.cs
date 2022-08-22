namespace TextParser.Logic.Interfaces;

public interface IFileManager
{
    string ReadFile(string path);
    IEnumerable<string> ReadFileLines(string path);
    IEnumerable<string> ReadFontLines();
    IEnumerable<string> ReadKeywordLines();
}