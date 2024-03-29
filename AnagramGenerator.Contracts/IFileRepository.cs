﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IFileRepository
    {
        HashSet<WordModel> GetWords();
        List<string> GetPageOfWords(int pageSize, int pageNumber);
        byte[] GetDictionaryFile();
    }
}
