using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Implementation;
using Interfaces;

namespace Anagram_Generator
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

            IAnagramSolver anagramSolver = new AnagramSolver(wordsRepository, userInputString);

            List<string> anagrams = anagramSolver.GetAnagrams();

            foreach(string anagram in anagrams)
            {
                Console.WriteLine(anagram);
            }
        }
    }
}
