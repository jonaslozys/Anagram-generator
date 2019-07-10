using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAnagramSolver
    {
        List<WordModel> GetAnagrams(string userInput);

    }
}
