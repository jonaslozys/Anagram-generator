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
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AnagramGenerator.Tests.Controllers
{
    public class DictionaryControllerTests
    {
        private IOptions<Dictionary> _dictionaryConfiguration;
        private IWordsRepository _wordsRepository;
        private IDictionaryService _dictionaryService;


        private IFileRepository _fileRepository;
        private DictionaryController _dictionaryController;

        [SetUp]
        public void Setup()
        {
            _dictionaryConfiguration = Substitute.For<IOptions<Dictionary>>();
            _dictionaryConfiguration.Value.Returns(new Dictionary() { pageSize = 100, path = " " });

            _wordsRepository = Substitute.For<IWordsRepository>();
            _dictionaryService = Substitute.For<IDictionaryService>();

            _fileRepository = Substitute.For<IFileRepository>();
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
        public void Should_Not_Call_Words_Repository_When_No_Word_Is_Received(string searchValue)
        {
            _wordsRepository.GetSearchedWords(searchValue).Returns(new List<WordModel> { });

            _dictionaryController.Search(searchValue);

            _wordsRepository.DidNotReceive().GetSearchedWords(searchValue);
        }

        [Test]
        [TestCase("randomWord")]
        [TestCase("otherWord")]
        public void Should_Return_View_With_Model_Containing_Some_Words(string searchValue)
        {
            _wordsRepository.GetSearchedWords(searchValue).Returns(new List<WordModel> { new WordModel(searchValue), new WordModel(searchValue) });

            ViewResult result = (ViewResult)_dictionaryController.Search(searchValue);


            DictionaryViewModel model = (DictionaryViewModel)result.Model;

            Assert.IsTrue(model.wordsDictionary.Count > 1);
        }

        [Test]
        [TestCase("someFileContents")]
        [TestCase("sdfjkbksfdbh")]
        [TestCase("reandomstuff")]
        public void Should_Return_Plan_Text_File(string fileContents)
        {
            _fileRepository.GetDictionaryFile().Returns(Encoding.ASCII.GetBytes(fileContents));
            FileContentResult result = (FileContentResult)_dictionaryController.Download();

            Assert.IsTrue(result.ContentType == "text/plain");
        }

    }
}
