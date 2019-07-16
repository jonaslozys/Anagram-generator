using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Ef.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.Ef.CodeFirst
{
    public class WordsEfCodeFirstRepository : IWordsRepository
    {
        private AnagramContext _dbContext;
        public WordsEfCodeFirstRepository(AnagramContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteWord(string word)
        {
            Word wordToDelete = _dbContext.Words.SingleOrDefault(w => w.WordValue == word);
            _dbContext.Words.Remove(wordToDelete);
            _dbContext.SaveChanges();
        }

        public List<string> GetPageOfWords(int pageSize, int pageNumber)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = (pageNumber) * pageSize;

            if (startIndex < 0) startIndex = 0;

            List<string> res = _dbContext.Words
                .Where(word => (word.Id > startIndex) && (word.Id < endIndex))
                .Select(word => word.WordValue)
                .ToList();

            return res;
        }

        public List<string> GetSearchedWords(string searchString)
        {
            List<string> results = _dbContext.Words
                .Where(word => word.WordValue.StartsWith(searchString))
                .Select(word => word.WordValue)
                .ToList();

            return results;
        }

        public HashSet<WordModel> GetWords()
        {
           HashSet<WordModel> _wordList = new HashSet<WordModel>();

            _dbContext.Words
                .AsNoTracking()
                .ToList()
                .ForEach(word => _wordList.Add(new WordModel(word.WordValue, word.Id)));

            return _wordList;
        }

        void IWordsRepository.AddNewWord(string word)
        {
            Word newWord = new Word() { WordValue = word };

            _dbContext.Add(newWord);
            _dbContext.SaveChanges();
        }
    }
}
