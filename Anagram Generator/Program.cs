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
            foreach(string word in args)
            {
                Console.WriteLine(word);
            }

            IWordsRepository wordsRepository = new WordsRepository();

            IAnagramSolver anagramSolver = new AnagramSolver(wordsRepository);

            HashSet<Word> words = anagramSolver.GetAnagrams();

        }
    }
}
