using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Data
{
    public class FileWordRepository : IWordRepository
    {
        private readonly string _filePath;

        public FileWordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public HashSet<WordModel> GetWords()
        {
            var words = new HashSet<WordModel>();

            var textLines = new List<string>();

            textLines = File.ReadAllLines(_filePath).ToList();

            foreach (string textLine in textLines)
            {
                string[] textLineArray = textLine.Split("\t");

                //zodynas.txt => 0     1        2    3
                //zodynas.txt => lemma wordForm word frequency
                string lemma = textLineArray[0];
                string wordForm = textLineArray[1];
                string word = textLineArray[2];
                int frequency = Int32.Parse(textLineArray[3]);

                words.Add(new WordModel 
                { 
                    Lemma = lemma.ToLower(), 
                    Form = wordForm.ToLower(), 
                    Word = word.ToLower(), 
                    Frequency = frequency 
                });
            }

            return words;
        }
    }
}