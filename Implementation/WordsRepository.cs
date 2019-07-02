using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation
{
    class WordsRepository
    {
        public List<Word> words = new List<Word>();

        public List<string> wordList = new List<string>();
        public void AddWord(string wordFromInput)
        {
            if(!wordList.Contains(wordFromInput))
            {
                wordList.Add(wordFromInput);
                Word word = new Word(wordFromInput);

                Console.WriteLine(wordFromInput);

                foreach (KeyValuePair<char, int> kvp in word.letterRegistry)
                {
                    Console.WriteLine("Key = {0}, Value = {1} ", kvp.Key, kvp.Value);

                }
            }
        }
    }
}
