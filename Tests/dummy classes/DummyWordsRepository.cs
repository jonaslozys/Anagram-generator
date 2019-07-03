using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Tests.dummy_classes
{
    public class DummyWordsRepository
    {
        public HashSet<Word> GetWords()
        {
            string[] words = new string[] { "pakeleivingas", "pakeleivingos", "pakeleivingos", "žvilgsnis", "seminaras", "Semionovičiumi" };

            HashSet<Word> wordObjects= new HashSet<Word>();

            foreach(string word in words)
            {
                wordObjects.Add(new Word(word));
            }

            return wordObjects;
        }
    }
}
