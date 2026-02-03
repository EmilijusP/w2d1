using AnagramSolver.BusinessLogic.Services;
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
                if (string.IsNullOrEmpty(textLine)) continue;

                string[] textLineArray = textLine.Split("\t");

                if (textLineArray.Length < 4) continue;

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

        public void WriteToFile(WordModel wordModel)
        {
            var line = new List<string> { $"{wordModel.Lemma}\t{wordModel.Form}\t{wordModel.Word}\t{wordModel.Frequency}" };
            File.AppendAllLines(_filePath, line);
        }
    }
}