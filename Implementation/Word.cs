using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    class Word
    {
        public string word;

        public Dictionary<char, int> letterRegistry = new Dictionary<char, int>();

        public Word(string word)
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
        }
    }
}
