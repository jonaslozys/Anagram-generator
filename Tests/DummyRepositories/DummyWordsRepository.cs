using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts.WebAPIResponseModels;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.Tests.DummyRepositories
{
    public class DummyWordsRepository : IWordsRepository
    {
        public void AddNewWord(string word)
        {
            throw new NotImplementedException();
        }

        public void DeleteWord(int wordId)
        {
            throw new NotImplementedException();
        }

        public List<WordResponseModel> GetPageOfWords(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public List<WordResponseModel> GetSearchedWords(string searchString)
        {
            throw new NotImplementedException();
        }

        public HashSet<WordModel> GetWords()
        {
            string[] words = new string[] {
                "pakeleivingas", "pakeleivingos", "pakeleivingos", "žvilgsnis", "seminaras", "Semionovičiumi",
                "menas", "nesam", "senam", "sala", "sula", "asla", "alus"
            };

            HashSet<WordModel> wordObjects= new HashSet<WordModel>();
            int index = 0;

            foreach(string word in words)
            {
                wordObjects.Add(new WordModel(word, index));
                index ++;
            }

            return wordObjects;
        }

        public void UpdateWord(int wordId, string word)
        {
            throw new NotImplementedException();
        }
    }
}
