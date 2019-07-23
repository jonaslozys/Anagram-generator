using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.BusinessLogic;
using WebApp;
using WebApp.Configuration;
using NUnit;
using NUnit.Framework;
using NSubstitute;
using WebApp.Controllers;
using Microsoft.Extensions.Options;

namespace AnagramGenerator.Tests.Controllers
{
    public class DictionaryControllerTests
    {
        private IOptions<Dictionary> _dictionaryConfiguration;
        private IWordsRepository _wordsRepository;
        private IDictionaryService _dictionaryService;

        private DictionaryController _dictionaryController;

        [SetUp]
        public void Setup()
        {
            _dictionaryConfiguration = Substitute.For<IOptions<Dictionary>>();
            _dictionaryConfiguration.Value.Returns(new Dictionary() { pageSize = 100, path = " " });

            _wordsRepository = Substitute.For<IWordsRepository>();
            _dictionaryService = Substitute.For<IDictionaryService>();

            _dictionaryController = new DictionaryController(_dictionaryConfiguration, _wordsRepository, _dictionaryService);
        }

        [Test]
        [TestCase(1, 100)]
        [TestCase(10, 200)]
        [TestCase(100, 50)]
        [TestCase(1000, 100)]
        public void Should_Call_Words_Repo_With_Correct_Arguments(int pageNumber, int pageSize)
        {
            _wordsRepository.GetPageOfWords(pageSize, pageNumber).Returns(new List<WordModel> { });

            _dictionaryController.Index(pageNumber);
            _wordsRepository.Received().GetPageOfWords(_dictionaryConfiguration.Value.pageSize, pageNumber);
            var res = _dictionaryController.Index(pageNumber);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Should_Not_Use_Call_Word_When_No_Word_Is_Received(string searchValue)
        {
            _wordsRepository.GetSearchedWords(searchValue).Returns(new List<WordModel> { });

            _dictionaryController.Search(searchValue);

            _wordsRepository.DidNotReceive().GetSearchedWords(searchValue);
        }
    }
}
