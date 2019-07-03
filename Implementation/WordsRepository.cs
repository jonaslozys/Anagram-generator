using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramLogic;
using Contracts;

namespace AnagramLogic
{
    public class WordsRepository : IWordsRepository
    {
        public HashSet<Word> GetLines()
        {
            HashSet<Word> wordList = new HashSet<Word>();

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

                    if (!wordList.Contains(word1))
                    {
                        wordList.Add(word1);
                    }

                    if (!wordList.Contains(word2))
                    {
                        wordList.Add(word2);
                    }
                }
            }

            return wordList;
        }
    }
}
