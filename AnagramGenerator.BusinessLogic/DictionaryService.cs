using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.BusinessLogic 
{
    public class DictionaryService : IDictionaryService
    {
        private IWordsRepository _wordsRepository;
        private IUsersRepository _usersRepository;

        public DictionaryService(IWordsRepository wordsRepository, IUsersRepository usersRepository)
        {
            _wordsRepository = wordsRepository;
            _usersRepository = usersRepository;
        }

        public void DeleteWord(string wordToDelete, string ip)
        {
            _wordsRepository.DeleteWord(wordToDelete);

            _usersRepository.DecreaseAvailabeUserSearches(ip);
        }

        public void AddWord(string wordToAdd, string ip)
        {
            _wordsRepository.AddNewWord(wordToAdd);

            _usersRepository.IncreaseAvailabeUserSearches(ip);
        }

        public void UpdateWord(string wordToUpdate, string ip, int wordIndex = -1)
        {
            _wordsRepository.UpdateWord(wordIndex, wordToUpdate);

            _usersRepository.IncreaseAvailabeUserSearches(ip);
        }
    }
}
