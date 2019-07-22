using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.BusinessLogic.Services 
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

            try
            {
                _wordsRepository.DeleteWord(wordToDelete);
                _usersRepository.DecreaseAvailabeUserSearches(ip);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddWord(string wordToAdd, string ip)
        {
            try
            {
                _wordsRepository.AddNewWord(wordToAdd);
                _usersRepository.IncreaseAvailabeUserSearches(ip);

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void UpdateWord(string wordToUpdate, string ip, int wordIndex = -1)
        {
            try
            {
                _wordsRepository.UpdateWord(wordIndex, wordToUpdate);
                _usersRepository.IncreaseAvailabeUserSearches(ip);

            } catch (Exception ex)
            {
                throw;
            }

        }
    }
}
