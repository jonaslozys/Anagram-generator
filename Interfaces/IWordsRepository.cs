using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IWordsRepository
    {
        HashSet<Word> GetWords();

        List<string> GetPageOfWords(int pageSize, int pageNumber);

        List<string> GetSearchedWords(string searchString);
        byte[] GetDictionaryFile();

    }
}
