using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Contracts;
using System.Linq;

namespace AnagramLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private IWordsRepository _wordsRepository;

        private HashSet<Word> _words;

        private Word _userInput;

        private List<string> _anagrams;
        public AnagramSolver(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
            _anagrams = new List<string>();
            _words = wordsRepository.GetLines();
        }

        private bool CompareWords(Word word1, Word word2)
        {
            if (word1.letterRegistry.Count != word2.letterRegistry.Count)
            {
                return false;

            } else
            {
                return word1.letterRegistry.Keys.All(key => {
                    if(word2.letterRegistry.ContainsKey(key))
                    {
                        return word2.letterRegistry[key] == word1.letterRegistry[key];
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        private void FindAnagrams()
        {
            foreach(Word word in _words)
            {
                if(CompareWords(_userInput, word))
                {
                    if(_userInput.word != word.word)
                    {
                        _anagrams.Add(word.word);
                    }
                }
            }
        }
        public List<string> GetAnagrams(string userInput)
        {
            _userInput = new Word(userInput);

            FindAnagrams();

            return _anagrams;
        }

    }
}
