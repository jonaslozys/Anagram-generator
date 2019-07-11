using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Anagram_Generator.EF.DatabaseFirst.Models;
using System.Linq;

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
            throw new NotImplementedException();
        }

        public List<string> GetPageOfWords(int pageSize, int pageNumber)
        {

            List<string> res = _anagramsContext.Words
                .Select(word => word.Word)
                //.Select((word, index) => new { word, index })
                //.Select(word => word.word)
                .ToList();
                //.Where(p => (p.index > pageSize * pageNumber) && (p.index < (pageSize * pageNumber) + pageSize))


            foreach (string word in res)
            {
                int i = 5;
            }

            List<string> results = new List<string>() { "sa", "das" };
            return results;
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
                    .ToList()
                    .ForEach(word => _wordList.Add(new WordModel(word.Word, word.Id)));

                return _wordList;
            }
        }
    }
}
