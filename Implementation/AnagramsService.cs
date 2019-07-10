using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace AnagramLogic
{
    public class AnagramsService : IAnagramsService
    {
        private IWordsRepository _wordsRepository;
        private ICacheRepository _cacheRepository;
        private IAnagramSolver _anagramSolver;

        private List<string> _cachedAnagrams;

        public AnagramsService(IWordsRepository wordsRepository, ICacheRepository cacheRepository, IAnagramSolver anagramSolver)
        {
            _wordsRepository = wordsRepository;
            _cacheRepository = cacheRepository;
            _anagramSolver = anagramSolver;
        }

        private bool IsCached(string word)
        {
            _cachedAnagrams = _cacheRepository.GetCachedAnagrams(word);

            return _cachedAnagrams.Count > 0;

        }

        public List<string> GetAnagrams(string word)
        {
            List<string> anagrams = new List<string>();

            if (IsCached(word))
            {
                anagrams = _cachedAnagrams;
            } else
            {
                anagrams = _anagramSolver.GetAnagrams(word);
                UpdateAnagramsCache(word, anagrams);
            }
            return anagrams;
        }

        private void UpdateAnagramsCache(string word, List<string> anagrams) 
        {
            _cacheRepository.UpdateAnagramsCache(word, anagrams);
        }



    }
}
