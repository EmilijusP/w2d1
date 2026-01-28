namespace AnagramSolver.Contracts.Models
{
    public class WordModel
    {
        public string? Lemma { get; set; }

        public string? Form { get; set; }

        public string? Word { get; set; }

        public int Frequency { get; set; }
    }
}
