using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;
using AnagramSolver.Cli;
using AnagramSolver.BusinessLogic.Services;

string jsonPath = "appsettings.json";
string content = File.ReadAllText(jsonPath);
AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(content);

var repository = new WordRepository(settings.FilePath);
var words = repository.GetWords();

var wordsValidation = new InputValidation();
var userInterface = new UserInterface(settings.MinWordLength, wordsValidation);

var anagramSolver = new AnagramSolverLogic();

Console.WriteLine(userInterface.ReadInput());

