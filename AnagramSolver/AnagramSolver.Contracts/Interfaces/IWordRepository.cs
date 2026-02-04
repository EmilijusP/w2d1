using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Task<HashSet<WordModel>> ReadAllLinesAsync(CancellationToken ct = default);

        Task WriteToFileAsync(WordModel wordModel, CancellationToken ct = default);
    }
}
