using AnagramSolver.Contracts.Models;
using System.ComponentModel;

namespace AnagramSolver.WebApp.Models
{
    public class AnagramViewModel
    {
        public string Word { get; set; }

        public IEnumerable<string>? Anagrams { get; set; } = new List<string>();

        public string? ErrorMessage { get; set; }

        public string? LastSearch { get; set; }
    }
}
