namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public IList<string> GetAnagrams(string userWords);
    }
}
