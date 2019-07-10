using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAnagramsService
    {
        List<string> GetAnagrams(string word);
    }
}
