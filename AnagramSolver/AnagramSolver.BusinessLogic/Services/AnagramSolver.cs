using AnagramSolver.BusinessLogic.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverLogic: IAnagramSolver
    {
        private readonly int _anagramCount;
        private readonly IWordRepository _wordRepository;
        private readonly Dictionary<string, List<string>> _dictionary;
        
        public AnagramSolverLogic(int anagramCount, IWordRepository wordRepository)
        {
            _anagramCount = anagramCount;
            _wordRepository = wordRepository;
            _dictionary = CreateAnagramDictionary(_wordRepository.GetWords());
        }

        public IList<string> GetAnagrams(string userWords)
        {
            string userInputKey = SortString(userWords.Replace(" ", ""));
            var userCharCount = CreateCharCountDictionary(userInputKey);

            var possibleKeys = _dictionary.Keys.Where(key => CanBeAnagram(key, userCharCount)).ToList();

            var keyCombinations = new List<List<string>>();
            FindKeyCombinations(userCharCount, _anagramCount, 0, possibleKeys, new List<string>(), keyCombinations);

            var results = new List<string>();
            foreach (var keyCombination in keyCombinations)
                CreateCombinations(keyCombination, 0, new List<string>(), results);
            return results;
        }

        private string SortString(string unsortedString)
        {
            unsortedString = unsortedString.Replace(" ", "");
            char[] arr = unsortedString.ToCharArray();
            Array.Sort(arr);
            string sortedString = new string(arr);
            return sortedString;
        }

        private Dictionary<string, List<string>> CreateAnagramDictionary(HashSet<WordModel> wordModels)
        {
            var dictionary = new Dictionary<string, List<string>>();
            foreach (var wordModel in wordModels)
            {
                string value = wordModel.Word;
                string key = SortString(value);
                if (!dictionary.ContainsKey(key))
                    dictionary[key] = new List<string>();

                if(!dictionary[key].Contains(value))
                    dictionary[key].Add(value);
            }

            return dictionary;  
        }

        private Dictionary<char, int> CreateCharCountDictionary(string characterString)
        {
            var charDictionary = new Dictionary<char, int>();

            foreach (var character in characterString)
            {
                if (!charDictionary.ContainsKey(character))
                    charDictionary[character] = 0;
                charDictionary[character]++;
            }

            return charDictionary;
        }

        private bool CanBeAnagram(string key, Dictionary<char, int> userCharCountDictionary)
        {
            var keyCharCountDictionary = CreateCharCountDictionary(key);

            foreach (var pair in keyCharCountDictionary)
            {
                if (!userCharCountDictionary.ContainsKey(pair.Key) || pair.Value > userCharCountDictionary[pair.Key])
                {
                    return false;
                }
            }

            return true;
        }

        private void FindKeyCombinations(Dictionary<char, int> remainingLetters, int wordsLeft, int startIndex, List<string> possibleKeys, List<string> currentCombination, List<List<string>> keyCombinations)
        {
            if (AllLettersUsed(remainingLetters))
            {
                keyCombinations.Add(new List<string>(currentCombination));
                return;
            }

            else if (wordsLeft == 0)
                return;
            
            for (int i = startIndex; i < possibleKeys.Count; i++)
            {
                string key = possibleKeys[i];
                if (KeyFits(key, remainingLetters))
                {
                    currentCombination.Add(key);
                    RemoveLetters(key, remainingLetters);

                    FindKeyCombinations(remainingLetters, wordsLeft - 1, i, possibleKeys, currentCombination, keyCombinations);

                    AddLetters(key, remainingLetters);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }

        }
        private bool AllLettersUsed(Dictionary<char, int> remainingLetters)
        {
            foreach (var pair in remainingLetters)
            {
                if (pair.Value > 0)
                    return false;
            }

            return true;
        }

        private bool KeyFits(string key, Dictionary<char, int> remainingLetters)
        {
            var keyCharCount = CreateCharCountDictionary(key);

            foreach (var pair in keyCharCount)
                if (pair.Value > remainingLetters[pair.Key])
                    return false;

            return true;
        }

        private void RemoveLetters(string key, Dictionary<char, int> charCountDictionary)
        {
            foreach (var character in key)
                charCountDictionary[character]--;
        }

        private void AddLetters(string key, Dictionary<char, int> charCountDictionary)
        {
            foreach (var character in key)
                charCountDictionary[character]++;
        }

        private void CreateCombinations(List<string> keyCombination, int index, List<string> currentSentence, List<string> results)
        {
            if (index == keyCombination.Count)
            {
                results.Add(string.Join(" ", currentSentence));
                return;
            }

            string currentKey = keyCombination[index];
            foreach (var wordVariant in _dictionary[currentKey])
            {
                currentSentence.Add(wordVariant);
                CreateCombinations(keyCombination, index + 1, currentSentence, results);
                currentSentence.RemoveAt(currentSentence.Count - 1);
            }
        }
    }
}
