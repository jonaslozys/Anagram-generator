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

        private AnagramConfiguration _configuration;

        private HashSet<WordModel> _words;

        private WordModel _userInput;

        private List<string> _anagrams;
        public AnagramSolver(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;
            _words = wordsRepository.GetWords();
            _anagrams = new List<string>();
        }

        private bool CompareWords(WordModel word1, WordModel word2)
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
            foreach(WordModel word in _words)
            {
                if(CompareWords(_userInput, word) && word.word.Length >= _configuration.minWordLength && _anagrams.Count < _configuration.maxResultsLength)
                {
                    if(_userInput.word != word.word)
                    {
                        _anagrams.Add(word.word);
                    }
                }
            }
        }
        public List<string> GetAnagrams(string userInput, AnagramConfiguration configuration)
        {
            _anagrams.Clear();

            _configuration = configuration;

            _userInput = new WordModel(userInput);

            FindAnagrams();

            return _anagrams;
        }

    }
}
