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

        public List<WordModel> GetPageOfWords(int pageSize, int pageNumber)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            if (startIndex < 0) startIndex = 0;

            List<WordModel> res = _dbContext.Words
                .OrderBy(word => word.WordValue)
                .Skip(startIndex)
                .Take(pageSize)
                .Select(word => new WordModel(word.WordValue, word.Id))
                .ToList();

            return res;
        }

        public List<WordModel> GetSearchedWords(string searchString)
        {
            List<WordModel> results = _dbContext.Words
                .Where(word => word.WordValue.StartsWith(searchString))
                .Select(word => new WordModel(word.WordValue, word.Id))
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

        public void UpdateWord(int wordId, string word)
        {
            Word wordToUpdate = _dbContext.Words.FirstOrDefault(w => w.Id == wordId);
            if (wordToUpdate != null)
            {
                wordToUpdate.WordValue = word;
                _dbContext.Update(wordToUpdate);
                _dbContext.SaveChanges();
            }
        }

        void IWordsRepository.AddNewWord(string word)
        {
            if (!_dbContext.Words.Any(w => w.WordValue == word)) {
                Word newWord = new Word() { WordValue = word };
                _dbContext.Add(newWord);
                _dbContext.SaveChanges();
            }
        }
    }
}
