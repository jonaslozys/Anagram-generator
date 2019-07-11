using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface ICacheRepository
    {
        List<WordModel> GetCachedAnagrams(string word);
        void UpdateAnagramsCache(string word, List<WordModel> anagrams);
    }
}
