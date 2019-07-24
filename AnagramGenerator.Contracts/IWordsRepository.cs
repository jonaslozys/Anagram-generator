using System;
using System.Collections.Generic;
using AnagramGenerator.Contracts.WebAPIResponseModels;

namespace AnagramGenerator.Contracts
{
    public interface IWordsRepository
    {
        HashSet<WordModel> GetWords();
        List<WordResponseModel> GetPageOfWords(int pageSize, int pageNumber);
        List<WordResponseModel> GetSearchedWords(string searchString);
        void DeleteWord(string word);
        void AddNewWord(string word);
        void UpdateWord(int wordId, string word);
    }
}
