using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnagramLogic;
using Contracts;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInputString = "";

            foreach(string word in args)
            {
                userInputString += word;
            }

            IWordsRepository wordsRepository = new WordsRepository();

            IAnagramSolver anagramSolver = new AnagramSolver(wordsRepository);

            List<string> anagrams = anagramSolver.GetAnagrams(userInputString);

            foreach(string anagram in anagrams)
            {
                Console.WriteLine(anagram);
            }
        }
    }
}
