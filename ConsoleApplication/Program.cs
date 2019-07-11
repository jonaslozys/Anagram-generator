using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnagramGenerator.BusinessLogic;
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

            int minWordLength = Int32.Parse(ConfigurationManager.AppSettings["minWordLength"]);
            int maxResultsLenth = Int32.Parse(ConfigurationManager.AppSettings["maxResultsLenth"]);

            AnagramConfiguration anagramConfiguration = new AnagramConfiguration(minWordLength, maxResultsLenth);

            IWordsRepository wordsRepository = new WordsRepository();

            IAnagramSolver anagramSolver = new AnagramSolver(wordsRepository, anagramConfiguration);

            List<string> anagrams = anagramSolver.GetAnagrams(userInputString);

            foreach(string anagram in anagrams)
            {
                Console.WriteLine(anagram);
            }

            Thread.Sleep(2000);
        }
    }
}
