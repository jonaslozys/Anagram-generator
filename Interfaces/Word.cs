using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public class Word
    {
        public string word;

        public Word(string word)
        {
            this.word = word;
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
