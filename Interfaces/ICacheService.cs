using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICacheService
    {
        bool IsCached(string word);
        List<string> GetCachedAnagrams();
        void UpdateAnagramsCache(string word, List<string> anagrams);
    }
}
