using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IWordsRepository
    {
        void AddWord(string word);

        List<Word> GetWords();

        List<string> GetRawWordList();
        
    }
}
