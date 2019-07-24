using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.WebAPIResponseModels;
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
        public void DeleteWord(int wordId)
        {
            Word wordToDelete = _dbContext.Words.SingleOrDefault(w => w.Id == wordId);
            _dbContext.Words.Remove(wordToDelete);
            _dbContext.SaveChanges();
        }

        public List<WordResponseModel> GetPageOfWords(int pageSize, int pageNumber)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            if (startIndex < 0) startIndex = 0;

            List<WordResponseModel> res = _dbContext.Words
                .OrderBy(word => word.WordValue)
                .Skip(startIndex)
                .Take(pageSize)
                .Select(word => new WordResponseModel(word.WordValue, word.Id))
                .ToList();

            return res;
        }

        public List<WordResponseModel> GetSearchedWords(string searchString)
        {
            List<WordResponseModel> results = _dbContext.Words
                .Where(word => word.WordValue.StartsWith(searchString))
                .Select(word => new WordResponseModel(word.WordValue, word.Id))
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

        public void UpdateWord(int wordId, string newValue)
        {
            try
            {
                Word wordToUpdate = _dbContext.Words.FirstOrDefault(w => w.Id == wordId);
                if (wordToUpdate != null)
                {
                    if (!_dbContext.Words.Any(w => w.WordValue == newValue))
                    {
                        wordToUpdate.WordValue = newValue;
                        _dbContext.Update(wordToUpdate);
                        _dbContext.SaveChanges();
                    } else
                    {
                        throw new InvalidOperationException("Word already exists.");
                    }


                } else
                {
                    throw new IndexOutOfRangeException("No word with ID provided was found.");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw;
            }
            catch(InvalidOperationException ex)
            {
                throw;
            }

        }

        void IWordsRepository.AddNewWord(string word)
        {
            try
            {
                if (!_dbContext.Words.Any(w => w.WordValue == word))
                {
                    Word newWord = new Word() { WordValue = word };
                    _dbContext.Add(newWord);
                    _dbContext.SaveChanges();
                } else
                {
                    throw new InvalidOperationException("Unable to add already existing word.");
                }
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }

        }
    }
}
