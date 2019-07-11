using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Anagram_Generator.EF.DatabaseFirst.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AnagramLogic
{
    public class EfWordsRepository : IEfWordsRepository
    {
        private AnagramsContext _anagramsContext;
        private HashSet<WordModel> _wordList;

        public EfWordsRepository(AnagramsContext anagramsContext)
        {
            _anagramsContext = anagramsContext;
            _wordList = new HashSet<WordModel>();
        }

        public void DeleteWord(string word)
        {
            Words wordToDelete = _anagramsContext.Words.SingleOrDefault(w => w.Word == word);
            _anagramsContext.Remove<Words>(wordToDelete);
            _anagramsContext.SaveChanges();
        }

        public List<string> GetPageOfWords(int pageSize, int pageNumber)
        {
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = (pageNumber) * pageSize;

            if (startIndex < 0) startIndex = 0;

            List<string> res = _anagramsContext.Words
                .Where(word => (word.Id > startIndex) && (word.Id < endIndex))
                .Select(word => word.Word)
                .ToList();

            return res;
        }

        public List<string> GetSearchedWords(string searchString)
        {
            List<string> results = _anagramsContext.Words
                .Where(word => word.Word.StartsWith(searchString))
                .Select(word => word.Word)
                .ToList();

            return results;
        }

        public HashSet<WordModel> GetWords()
        {
            if (_wordList.Count > 1)
            {
                return _wordList;

            }
            else
            {
                _wordList = new HashSet<WordModel>();

                _anagramsContext.Words
                    .AsNoTracking()
                    .ToList()
                    .ForEach(word => _wordList.Add(new WordModel(word.Word, word.Id)));

                return _wordList;
            }
        }
    }
}
