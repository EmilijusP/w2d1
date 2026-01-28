using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverLogic: IAnagramSolver
    {
        public IList<string> GetAnagrams(string userWords)
        {
            return new List<string>();
        }
    }
}
