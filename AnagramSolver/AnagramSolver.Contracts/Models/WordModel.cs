namespace AnagramSolver.Contracts.Models
{
    public class WordModel
    {
        public string? Lemma { get; set; } = "-";

        public string? Form { get; set; } = "-";

        public string? Word { get; set; } = string.Empty;

        public int Frequency { get; set; } = 1;

    }
}
