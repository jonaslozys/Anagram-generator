using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramSolver
    {
        List<string> GetAnagrams(string userInput);

    }
}
