using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Ef.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.Ef.CodeFirst
{
    public class CacheEfCodeFirstRepository : ICacheRepository
    {
        private AnagramContext _dbContext;

        public CacheEfCodeFirstRepository(AnagramContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<WordModel> GetCachedAnagrams(string word)
        {
            List<WordModel> anagrams = _dbContext.CachedWords
                .Where(c => c.Word == word)
                .Select(w => new WordModel(w.AnagramWord.WordValue))
                .ToList();

            return anagrams;
        }

        public void UpdateAnagramsCache(string word, List<WordModel> anagrams)
        {
            foreach (WordModel anagram in anagrams)
            {
                _dbContext.Add(new CachedWord() { Word = word,  AnagramWord = _dbContext.Words.SingleOrDefault(w => w.Id == anagram.Id)});
            }

            _dbContext.SaveChanges();
        }
    }
}
