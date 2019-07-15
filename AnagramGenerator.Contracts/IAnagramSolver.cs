using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramSolver
    {
        List<WordModel> GetAnagrams(string userInput, HashSet<WordModel> words);

    }
}
