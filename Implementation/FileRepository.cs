using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Contracts;

namespace AnagramLogic
{
    public class FileRepository : IFileRepository
    {
        private HashSet<WordModel> _wordList;
        private string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";

        public HashSet<WordModel> GetWords()
        {
            _wordList = new HashSet<WordModel>();


            Environment.CurrentDirectory = target;

            using (StreamReader sr = new StreamReader($@"{target}/zodynas.txt"))
            {
                string line;
                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine().Trim();
                    string[] words = line.Split(new[] { '\t' });
                    WordModel word1 = new WordModel(words[0]);
                    WordModel word2 = new WordModel(words[2]);

                    if (!_wordList.Contains(word1))
                    {
                        _wordList.Add(word1);
                    }

                    if (!_wordList.Contains(word2))
                    {
                        _wordList.Add(word2);
                    }
                }
            }

            return _wordList;
        }

        public List<string> GetPageOfWords(int pageSize, int pageNumber)
        {
            List<string> pageOfWords = this.GetWords()
                .Select(word => word.word)
                .ToList();

            int index = (pageNumber - 1) * pageSize;
            int count = pageSize;

            if (index < 0) index = 0;
            if ((index + pageSize) > pageOfWords.Count)
            {
                index = pageOfWords.Count - pageSize;
                count = pageSize;
            }

            pageOfWords = pageOfWords
                .GetRange(index, count)
                .ToList();

            return pageOfWords;
        }

        public byte[] GetDictionaryFile()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes($@"{target}/zodynas.txt");
            return fileBytes;

        }
    }
}
