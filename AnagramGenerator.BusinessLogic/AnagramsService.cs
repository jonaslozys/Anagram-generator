using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.EF.DatabaseFirst;
using System.Linq;

namespace AnagramGenerator.BusinessLogic
{
    public class AnagramsService : IAnagramsService
    {
        //private ICacheRepository _cacheRepository;
        private IAnagramSolver _anagramSolver;
        private EfWordsRepository _efWordsRepository;
        private EfCacheRepository _efCacheRepository;

        private List<WordModel> _cachedAnagrams;

        public AnagramsService(EfWordsRepository efWordsRepository, EfCacheRepository cacheRepository, IAnagramSolver anagramSolver)
        {
            //_cacheRepository = cacheRepository;
            _efCacheRepository = cacheRepository;
            _efWordsRepository = efWordsRepository;
            _anagramSolver = anagramSolver;
        }

        private bool IsCached(string word)
        {
            _cachedAnagrams = _efCacheRepository.GetCachedAnagrams(word);

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
                anagrams = _anagramSolver.GetAnagrams(word, _efWordsRepository.GetWords());
                UpdateAnagramsCache(word, anagrams);
            }
            return anagrams.Select(w => w.word).ToList();
        }

        private void UpdateAnagramsCache(string word, List<WordModel> anagrams) 
        {
            _efCacheRepository.UpdateAnagramsCache(word, anagrams);
        }



    }
}
