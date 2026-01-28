using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverLogic: IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        
        public AnagramSolverLogic(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IList<string> GetAnagrams(string userWords)
        {
            int wordCount = userWords.Split().Count();
            HashSet<WordModel> wordModels = _wordRepository.GetWords();


            return new List<string>();
        }

        private string SortString(string unsortedString)
        {
            unsortedString = unsortedString.Replace(" ", "");
            char[] arr = unsortedString.ToCharArray();
            Array.Sort(arr);
            string sortedString = new string(arr);
            return sortedString;
        }

        private Dictionary<string, List<string>> CreateDictionary()
        {

            return new Dictionary<string, List<string>>();
        }
    }
}
