using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.Contracts.configurations;
using AnagramGenerator.BusinessLogic.Services;
using Tests.DummyRepositories;
using NUnit;
using NUnit.Framework;
using NSubstitute;
using Microsoft.Extensions.Options;

namespace Tests.Services
{
    public class AnagramServiceTests
    {
        private IAnagramSolver _anagramSolver;
        private IWordsRepository _wordsRepository;
        private ICacheRepository _cacheRepository;
        private IUsersRepository _usersRepository;
        private DummyWordsRepository _dummyWordsRepository;

        private AnagramsService _anagramsService;

        [SetUp]
        public void Setup()
        {
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _wordsRepository = Substitute.For<IWordsRepository>();
            _cacheRepository = Substitute.For<ICacheRepository>();
            _usersRepository = Substitute.For<IUsersRepository>();

            _dummyWordsRepository = new DummyWordsRepository();

            _anagramsService = new AnagramsService(_wordsRepository, _cacheRepository, _usersRepository, _anagramSolver);
        }

        [Test]
        [TestCase("Alus", "::1")]
        [TestCase("Sula", "::1")]
        [TestCase("Sala", "::1")]
        public void Should_Use_Anagrams_Cache_Only(string word, string userIp)
        {
            _cacheRepository.GetCachedAnagrams(word).Returns(new List<WordModel>() { new WordModel(word) });
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());
            _anagramSolver.GetAnagrams(word, _dummyWordsRepository.GetWords()).Returns(new List<WordModel>() { new WordModel(word) });

            _anagramsService.GetAnagrams(word, userIp);

            _cacheRepository.Received().GetCachedAnagrams(word);
            _wordsRepository.DidNotReceive().GetWords();
            _anagramSolver.DidNotReceive().GetAnagrams(word, _wordsRepository.GetWords());
        }


        [Test]
        [TestCase("Alus", "::1")]
        [TestCase("Sula", "::1")]
        [TestCase("Sala", "::1")]
        public void Should_Use_Anagram_Solver_And_Return_At_Least_One_Anagram(string word, string userIp)
        {
            _cacheRepository.GetCachedAnagrams(word).Returns(new List<WordModel>());
            _wordsRepository.GetWords().Returns(_dummyWordsRepository.GetWords());
            _anagramSolver.GetAnagrams(word, _wordsRepository.GetWords()).Returns(new List<WordModel>() { new WordModel(word) });
            
            Assert.IsTrue(_anagramsService.GetAnagrams(word, userIp).Count >= 1);

            _cacheRepository.Received().GetCachedAnagrams(word);
            _wordsRepository.Received().GetWords();
            _anagramSolver.Received().GetAnagrams(word, _wordsRepository.GetWords());

        }

    }
}
