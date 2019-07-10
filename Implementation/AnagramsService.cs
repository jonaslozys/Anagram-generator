using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;

namespace AnagramLogic
{
    public class AnagramsService : IAnagramsService
    {
        private ICacheRepository _cacheRepository;
        private IAnagramSolver _anagramSolver;

        private List<WordModel> _cachedAnagrams;

        public AnagramsService(ICacheRepository cacheRepository, IAnagramSolver anagramSolver)
        {
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
            List<WordModel> anagrams;

            if (IsCached(word))
            {
                anagrams = _cachedAnagrams;
            } else
            {
                anagrams = _anagramSolver.GetAnagrams(word);
                UpdateAnagramsCache(word, anagrams);
            }
            return anagrams.Select(w => w.word).ToList();
        }

        private void UpdateAnagramsCache(string word, List<WordModel> anagrams) 
        {
            _cacheRepository.UpdateAnagramsCache(word, anagrams);
        }



    }
}
