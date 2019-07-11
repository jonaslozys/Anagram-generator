using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public class WordModel
    {
        public string word;

        public Dictionary<char, int> letterRegistry = new Dictionary<char, int>();

        public int Id { get; set; }

        public WordModel(string word)
        {
            this.word = word;

            foreach(char letter in word)
            {
                if(letterRegistry.ContainsKey(letter))
                {
                    letterRegistry[letter] += 1;
                } else
                {
                    letterRegistry[letter] = 1;
                }
            }
        }

        public WordModel(string word, int id)
        {
            this.word = word;

            foreach (char letter in word)
            {
                if (letterRegistry.ContainsKey(letter))
                {
                    letterRegistry[letter] += 1;
                }
                else
                {
                    letterRegistry[letter] = 1;
                }
            }

            Id = id;
        }

        public override bool Equals(object obj)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return this.word.GetHashCode();
        }

    }
}
