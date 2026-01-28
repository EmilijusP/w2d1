using AnagramSolver.Contracts.Models;
using AnagramSolver.BusinessLogic.Data;

using System.Text.Json;
using System.Text.Json.Serialization;

string jsonPath = "appsettings.json";
string content = File.ReadAllText(jsonPath);
AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(content);

var repository = new WordRepository(settings.FilePath);
var words = repository.GetWords();

