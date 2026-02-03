using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        HashSet<WordModel> GetWords();

        void WriteToFile(WordModel wordModel);
    }
}
