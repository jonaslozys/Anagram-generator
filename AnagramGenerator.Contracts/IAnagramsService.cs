using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramsService
    {
        List<string> GetAnagrams(string word, string ip);
    }
}
