using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Implementation
{
    class WordsRepository : IWordsRepository
    {
        public List<Word> words = new List<Word>();

        public List<string> wordList = new List<string>();
        public void AddWord(string wordFromInput)
        {
            if(!wordList.Contains(wordFromInput))
            {
                Word word = new Word(wordFromInput);
                words.Add(word);
                wordList.Add(wordFromInput);

                Console.WriteLine(wordFromInput);

                foreach (KeyValuePair<char, int> kvp in word.letterRegistry)
                {
                    Console.WriteLine("Key = {0}, Value = {1} ", kvp.Key, kvp.Value);

                }
            }
        }

        public List<Word> GetWords()
        {
            return this.words;
        } 

        public List<string> GetRawWordList()
        {
            return this.wordList;
        }

        List<Interfaces.Word> IWordsRepository.GetWords()
        {
            throw new NotImplementedException();
        }
    }
}
