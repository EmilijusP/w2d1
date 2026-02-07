using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Api.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<string>> GetAnagramsAsync(string word, [Service]IAnagramSolver anagramSolver, CancellationToken ct)
        {
            return await anagramSolver.GetAnagramsAsync(word, ct);
        }

        public async Task<IEnumerable<WordModel>> GetWordsAsync([Service]IWordRepository wordRepository, CancellationToken ct)
        {
            return await wordRepository.ReadAllLinesAsync(ct);
        }
    }
}
