using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IWordsRepository
    {
        HashSet<Word> GetLines();

    }
}
