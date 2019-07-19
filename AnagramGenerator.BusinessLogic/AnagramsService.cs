﻿using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.EF.DatabaseFirst;
using AnagramGenerator.Ef.CodeFirst;
using System.Linq;

namespace AnagramGenerator.BusinessLogic
{
    public class AnagramsService : IAnagramsService
    {
        private IAnagramSolver _anagramSolver;
        private IWordsRepository _wordsRepository;
        private ICacheRepository _cacheRepository;
        private IUsersRepository _usersRepository;

        private List<WordModel> _cachedAnagrams;

        public AnagramsService(IWordsRepository wordsRepository, ICacheRepository cacheRepository, IUsersRepository usersRepository, IAnagramSolver anagramSolver)
        {
            _wordsRepository = wordsRepository;
            _cacheRepository = cacheRepository;
            _usersRepository = usersRepository;
            _anagramSolver = anagramSolver;
        }

        private bool IsCached(string word)
        {
            _cachedAnagrams = _cacheRepository.GetCachedAnagrams(word);

            return _cachedAnagrams.Count > 0;

        }

        public List<string> GetAnagrams(string word, string ip)
        {
            try
            {
                _usersRepository.DecreaseAvailabeUserSearches(ip);

            } catch (Exception ex)
            {
                throw;
            }

            List<WordModel> anagrams;

            if (IsCached(word))
            {
                anagrams = _cachedAnagrams;
            } else
            {
                anagrams = _anagramSolver.GetAnagrams(word, _wordsRepository.GetWords());
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
