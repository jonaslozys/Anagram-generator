using System;
using System.Collections.Generic;

namespace Contracts
{
    public interface IWordsRepository
    {
        HashSet<Word> GetLines();

    }
}
