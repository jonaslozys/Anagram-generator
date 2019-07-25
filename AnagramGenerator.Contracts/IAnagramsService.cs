using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts.WebAPIResponseModels;

namespace AnagramGenerator.Contracts
{
    public interface IAnagramsService
    {
        List<WordResponseModel> GetAnagrams(string word, string ip);
    }
}
