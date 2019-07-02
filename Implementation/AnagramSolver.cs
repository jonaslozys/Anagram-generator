using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Interfaces;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        IWordsRepository wordsRepository;

        private HashSet<Word> words; 
        public AnagramSolver(IWordsRepository wordsRepository)
        {
            this.wordsRepository = wordsRepository;
            this.words = wordsRepository.GetLines();
        }
        public HashSet<Word> GetAnagrams()
        {
            return this.words;
        }
    }
}
