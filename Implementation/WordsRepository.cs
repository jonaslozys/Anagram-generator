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

        public HashSet<Word> GetWords()
        {
            _wordList = new HashSet<Word>();

            string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";

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
                .ToList()
                .GetRange(pageNumber - 1 * pageSize, pageSize)
                .Select(word => word.word)
                .ToList();

            return pageOfWords;
        }

    }
}
