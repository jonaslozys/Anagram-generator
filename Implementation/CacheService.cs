using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace AnagramLogic
{
    public class CacheService : ICacheService
    {
        private IWordsRepository _wordsRepository;

        private List<string> _cachedAnagrams;

        public CacheService(IWordsRepository wordsRepository)
        {
            _wordsRepository = wordsRepository;

        }
        public bool IsCached(string word)
        {
            _cachedAnagrams = _wordsRepository.GetCachedAnagrams(word);

            return _cachedAnagrams.Count > 0;

        }

        public List<string> GetCachedAnagrams()
        {
            return _cachedAnagrams;
        }

        public void UpdateAnagramsCache(string word, List<string> anagrams) 
        {
            _wordsRepository.UpdateAnagramsCache(word, anagrams);
        }



    }
}
