using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.BusinessLogic.Services;
using AnagramGenerator.Tests.DummyRepositories;
using NUnit;
using NSubstitute;
using AnagramGenerator.Contracts;
using NUnit.Framework;
using NSubstitute.ExceptionExtensions;

namespace AnagramGenerator.Tests.Services
{
    public class DictionaryServiceTests
    {
        private IWordsRepository _wordsRepository;
        private IUsersRepository _usersRepository;

        private DummyWordsRepository _dummyWordsRepository;

        private DictionaryService _dictionaryService;

        [SetUp]
        public void Setup()
        {
            _wordsRepository = Substitute.For<IWordsRepository>();
            _usersRepository = Substitute.For<IUsersRepository>();
            _dummyWordsRepository = new DummyWordsRepository();

            _dictionaryService = new DictionaryService(_wordsRepository, _usersRepository);
        }

        [Test]
        [TestCase("qwerty123456789", "::1")]
        [TestCase("aluss", "::1")]
        [TestCase("rrrrrrrrr", "::1")]
        public void Should_Throw_Exception_Trying_To_Delete_Non_Existing_Word(string word, string userIp)
        {
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());

            _wordsRepository
                .When(x => x.DeleteWord(word))
                .Do(x => {
                    if(!_wordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.Throws<Exception>(() => _dictionaryService.DeleteWord(word, userIp));
        }

        [Test]
        [TestCase("nesam", "::1")]
        [TestCase("alus", "::1")]
        [TestCase("asla", "::1")]
        public void Should_Not_Throw_Exception_Trying_To_Delete_Existing_Word(string word, string userIp)
        {
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());

            _wordsRepository
                .When(x => x.DeleteWord(word))
                .Do(x => {
                    if (!_wordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.DoesNotThrow(() => _dictionaryService.DeleteWord(word, userIp));
        }

        [Test]
        [TestCase("qwerty123456789", "::1")]
        [TestCase("aluss", "::1")]
        [TestCase("rrrrrrrrr", "::1")]
        public void Does_Not_Decrease_Available_Searches_When_Failing_To_Delete_Word(string word, string userIp)
        {
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());

            _wordsRepository
                .When(x => x.DeleteWord(word))
                .Do(x => {
                    if (!_wordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.Throws<Exception>(() => _dictionaryService.DeleteWord(word, userIp));

            _usersRepository.DidNotReceive().DecreaseAvailabeUserSearches(userIp);
        }

        [Test]
        [TestCase("nesam", "::1")]
        [TestCase("alus", "::1")]
        [TestCase("asla", "::1")]
        public void Should_Throw_Exception_Trying_To_Add_Existing_Word(string word, string userIp)
        {

            _wordsRepository
                .When(x => x.AddNewWord(word))
                .Do(x => {
                    if (_dummyWordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.Throws<Exception>(() => _dictionaryService.AddWord(word, userIp));
        }

        [Test]
        [TestCase("qwerty123456789", "::1")]
        [TestCase("aluss", "::1")]
        [TestCase("rrrrrrrrr", "::1")]
        public void Should_Not_Throw_Exception_Trying_To_New_Word(string word, string userIp)
        {
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());

            _wordsRepository
                .When(x => x.AddNewWord(word))
                .Do(x => {
                    if (_dummyWordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.DoesNotThrow(() => _dictionaryService.AddWord(word, userIp));
        }

        [Test]
        [TestCase("nesam", 7, "::1")]
        [TestCase("alus", 2, "::1")]
        [TestCase("asla", 11, "::1")]
        public void Should_Throw_Exception_Trying_To_Update_Word_To_Existing_Word(string word, int wordIndex, string userIp)
        {

            _wordsRepository
                .When(x => x.UpdateWord(wordIndex, word))
                .Do(x => {
                    if (_dummyWordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.Throws<Exception>(() => _dictionaryService.UpdateWord(word, userIp, wordIndex));
        }

        [Test]
        [TestCase("sulcda", 45, "::1")]
        [TestCase("alcdsus", 12, "::1")]
        [TestCase("rrrrrrrrr", 4, "::1")]
        public void Should_Not_Throw_Exception_Trying_To_Update_Word_To_New_Word(string word, int wordIndex, string userIp)
        {
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());

            _wordsRepository
                .When(x => x.UpdateWord(wordIndex, word))
                .Do(x => {
                    if (_dummyWordsRepository.GetWords().Contains(new WordModel(word)))
                    {
                        throw new Exception();
                    }
                });

            Assert.DoesNotThrow(() => _dictionaryService.UpdateWord(word, userIp, wordIndex));
        }


    }
}
