using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Services;

namespace AnagramSolver.Cli
{
    public class UserInterface
    {
        private readonly int _minInputWordLength;
        private InputValidation _inputValidation;

        public UserInterface(int minInputWordLength, InputValidation inputValidation)
        {
            _minInputWordLength = minInputWordLength;
            _inputValidation = inputValidation;
        }

        public string ReadInput()
        {
            do
            {
                Console.WriteLine($"Enter the word/words containing {_minInputWordLength} letters or more: ");
                var input = Console.ReadLine();
                if (_inputValidation.IsValidUserInput(input, _minInputWordLength))
                {
                    return input;
                }

            } while (true);
        }

        public void ShowOutput(IEnumerable<string> words)
        {
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
