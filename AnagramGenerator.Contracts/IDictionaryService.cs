using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IDictionaryService
    {
        void DeleteWord(int wordId, string ip);
        void AddWord(string wordToAdd, string ip);
        void UpdateWord(string wordToUpdate, string ip, int wordIndex);
    }
}
