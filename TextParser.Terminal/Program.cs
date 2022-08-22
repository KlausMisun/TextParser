// See https://aka.ms/new-console-template for more information

using TextParser.Infrastructure;
using TextParser.Logic;

Console.WriteLine("Hello, World!");
var fileInput = File.ReadAllText(args[0]);

var result = new ParseTextHandler(new FileManager()).Do(new ParseTextRequest(fileInput));

Console.WriteLine(result);