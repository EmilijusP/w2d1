using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Data
{
    public class WordRepository : IWordRepository
    {
        private readonly string _filePath;

        public WordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public HashSet<WordModel> GetWords()
        {
            var words = new HashSet<WordModel>();

            var textLines = new List<string> { };

            textLines = File.ReadAllLines(_filePath).ToList();

            foreach (string textLine in textLines)
            {
                foreach (string word in textLine.Split())
                {
                    words.Add(new WordModel { Word = word.ToLower() });
                }
            }

            return words;
        }
    }
}