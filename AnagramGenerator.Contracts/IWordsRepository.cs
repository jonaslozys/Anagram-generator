using System;
using System.Collections.Generic;

namespace AnagramGenerator.Contracts
{
    public interface IWordsRepository
    {
        HashSet<WordModel> GetWords();
        List<WordModel> GetPageOfWords(int pageSize, int pageNumber);
        List<WordModel> GetSearchedWords(string searchString);
        void DeleteWord(string word);
        void AddNewWord(string word);
        void UpdateWord(int wordId, string word);
    }
}
