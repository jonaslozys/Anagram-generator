using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    class WordsRepository
    {
        public List<Word> words = new List<Word>();

        public void AddWord(string wordFromInput)
        {
            Word word = new Word(wordFromInput);

            foreach (KeyValuePair<char, int> kvp in word.letterRegistry)
            {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);

            }
        }
    }
}
