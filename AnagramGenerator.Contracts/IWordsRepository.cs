using System;
using System.Collections.Generic;

namespace AnagramGenerator.Contracts
{
    public interface IWordsRepository
    {
        HashSet<Word> GetWords();

    }
}
