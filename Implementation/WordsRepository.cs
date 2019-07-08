using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramLogic;
using Contracts;
using System.Linq;

namespace AnagramLogic
{
    public class WordsRepository : IWordsRepository
    {
        private HashSet<Word> _wordList;
        private string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";

        public HashSet<Word> GetWords()
        {
            _wordList = new HashSet<Word>();


            Environment.CurrentDirectory = target;

            using (StreamReader sr = new StreamReader($@"{target}/zodynas.txt"))
            {
                string line;
                while (sr.Peek() >= 0)
                {
                    line = sr.ReadLine().Trim();
                    string[] words = line.Split(new[] { '\t' });
                    Word word1 = new Word(words[0]);
                    Word word2 = new Word(words[2]);

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
            if((index + pageSize) > pageOfWords.Count )
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
