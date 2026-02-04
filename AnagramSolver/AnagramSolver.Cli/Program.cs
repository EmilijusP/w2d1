using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;
using AnagramSolver.BusinessLogic.Services;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) => {
    e.Cancel = true;
    cts.Cancel();
};

string jsonPath = "appsettings.json";
string content = await File.ReadAllTextAsync(jsonPath);

var settings = JsonSerializer.Deserialize<AppSettings>(content);

var wordRepository = new FileWordRepository(settings.FilePath);

var wordProcessor = new WordProcessor();

var inputValidation = new InputValidation(wordRepository, wordProcessor);

var anagramDictionary = new AnagramDictionaryService(wordProcessor, wordRepository, inputValidation);

var anagramAlgorithm = new AnagramAlgorithm();

var anagramSolver = new AnagramSolverService(wordProcessor, anagramDictionary, anagramAlgorithm, wordRepository, settings.AnagramCount, settings.MinOutputWordsLength);

var ui = new UserInterface(settings.MinInputWordsLength, inputValidation);

var userInput = ui.ReadInput();
var results = await anagramSolver.GetAnagramsAsync(userInput, cts.Token);
ui.ShowOutput(results);