using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.configurations;
using AnagramGenerator.EF.DatabaseFirst.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.EF.DatabaseFirst
{
    public class EfCacheRepository
    {
        private AnagramsContext _dbContext;

        public EfCacheRepository(AnagramsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WordModel> GetCachedAnagrams(string word)
        {
            List<WordModel> anagrams = new List<WordModel>();

            List<CachedWords> cachedWords = _dbContext.CachedWords
                .Where(w => w.Word == word)
                .AsNoTracking()
                .ToList();

            if (cachedWords.Count > 0)
            {
                foreach(CachedWords cachedWord in cachedWords)
                {
                    Words anagram = _dbContext.Words.SingleOrDefault(w => w.Id == cachedWord.Id);

                    anagrams.Add(new WordModel(anagram.Word));
                }
            }

            return anagrams;
        }

        public void UpdateAnagramsCache(string word, List<WordModel> anagrams)
        {
            foreach(WordModel anagram in anagrams)
            {
                _dbContext.Add(new CachedWords() { Word = word, Id = anagram.Id });
            }

            _dbContext.SaveChanges();
        }

    }
}
