using System;
using System.Collections.Generic;

namespace AnagramGenerator.Contracts
{
    public interface IWordsRepository
    {
        HashSet<WordModel> GetWords();

        List<string> GetPageOfWords(int pageSize, int pageNumber);
        List<string> GetSearchedWords(string searchString);
        void DeleteWord(string word);

    }
}
