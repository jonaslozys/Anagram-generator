using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.configurations;
using AnagramGenerator.EF.DatabaseFirst;
using System.Linq;
using Microsoft.Extensions.Options;

namespace AnagramGenerator.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        //private IWordsRepository _wordsRepository;

        //private EfWordsRepository _efWordsRepository;

        private AnagramConfiguration _configuration;

        private HashSet<WordModel> _words;

        private WordModel _userInput;

        private List<WordModel> _anagrams;
        public AnagramSolver(IOptionsMonitor<AnagramConfiguration> configuration)
        {
            //_wordsRepository = wordsRepository;
            //_efWordsRepository = efWordsRepository;
            //_words = efWordsRepository.GetWords();
            _configuration = configuration.CurrentValue;
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
                        _anagrams.Add(word);
                    }
                }
            }
        }
        public List<WordModel> GetAnagrams(string userInput, HashSet<WordModel> words)
        {
            _anagrams = new List<WordModel>();
            _words = words;

            _userInput = new WordModel(userInput);

            FindAnagrams();

            return _anagrams;
        }

    }
}
