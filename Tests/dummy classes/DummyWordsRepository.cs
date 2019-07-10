using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Tests.dummy_classes
{
    public class DummyWordsRepository : IWordsRepository
    {
        public HashSet<WordModel> GetWords()
        {
            string[] words = new string[] {
                "pakeleivingas", "pakeleivingos", "pakeleivingos", "žvilgsnis", "seminaras", "Semionovičiumi",
                "menas", "nesam", "senam"
            };

            HashSet<WordModel> wordObjects= new HashSet<WordModel>();

            foreach(string word in words)
            {
                wordObjects.Add(new WordModel(word));
            }

            return wordObjects;
        }
    }
}
