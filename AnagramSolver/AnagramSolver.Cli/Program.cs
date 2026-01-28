using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;
using AnagramSolver.BusinessLogic.Services;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;


string jsonPath = "appsettings.json";
string content = File.ReadAllText(jsonPath);
var settings = JsonSerializer.Deserialize<AppSettings>(content);
var wordsValidation = new InputValidation();
var repository = new WordRepository(settings.FilePath);
var anagramSolver = new AnagramSolverLogic(repository);
var ui = new UserInterface(settings.MinWordLength, wordsValidation);

//var userInput = ui.ReadInput();
//var results = anagramSolver.GetAnagrams(userInput);
//ui.ShowOutput(results);

var test = repository.GetWords();
foreach (var word in test.ToList())
{
    Console.WriteLine($"Lemma: {word.Lemma}, Form: {word.Form}, Word: {word.Word}, Frequency: {word.Frequency}");
}