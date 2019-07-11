using System;
using System.Collections.Generic;
using System.Text;
using Anagram_Generator.EF.DatabaseFirst.Models;

namespace AnagramGenerator.Contracts
{
    public interface IEfWordsRepository
    {
        HashSet<WordModel> GetWords();
        List<string> GetPageOfWords(int pageSize, int pageNumber);
        List<string> GetSearchedWords(string searchString);
        void DeleteWord(string word);
    }
}
