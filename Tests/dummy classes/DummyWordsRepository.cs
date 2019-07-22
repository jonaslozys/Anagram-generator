using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;

namespace Tests.dummy_classes
{
    public class DummyWordsRepository : IWordsRepository
    {
        public void AddNewWord(string word)
        {
            throw new NotImplementedException();
        }

        public void DeleteWord(string word)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetPageOfWords(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetSearchedWords(string searchString)
        {
            throw new NotImplementedException();
        }

        public HashSet<WordModel> GetWords()
        {
            string[] words = new string[] {
                "pakeleivingas", "pakeleivingos", "pakeleivingos", "žvilgsnis", "seminaras", "Semionovičiumi",
                "menas", "nesam", "senam", "sala", "sula", "asla", ""
            };

            HashSet<WordModel> wordObjects= new HashSet<WordModel>();

            foreach(string word in words)
            {
                wordObjects.Add(new WordModel(word));
            }

            return wordObjects;
        }

        public void UpdateWord(int wordId, string word)
        {
            throw new NotImplementedException();
        }
    }
}
