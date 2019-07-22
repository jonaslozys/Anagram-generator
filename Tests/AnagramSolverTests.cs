using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.BusinessLogic;
using System.Configuration;
using System.Linq;
using AnagramGenerator.Tests.DummyRepositories;
using Microsoft.Extensions.Options;
using AnagramGenerator.Contracts.configurations;


namespace AnagramGenerator.Tests
{
    class AnagramSolverTests
    {

        private IWordsRepository wordsRepository;
        private IOptions<AnagramConfiguration> config;


        [SetUp]
        public void SetUp()
        {
            wordsRepository = new DummyWordsRepository();
            config = Options.Create<AnagramConfiguration>(new AnagramConfiguration() { minWordLength = 3, maxResultsLength = 15 });
        }

        [Test]
        [TestCase("menas")]
        [TestCase("nesam")]
        [TestCase("senam")]
        public void Should_Return_Two_Anagrams(string userInput)
        {
            AnagramSolver anagramSolver = new AnagramSolver(config);
            List<WordModel> anagrams = anagramSolver.GetAnagrams(userInput ,wordsRepository.GetWords());
            Assert.IsTrue(anagrams.Count() == 2);
        }

        [Test]
        [TestCase("menas")]
        [TestCase("nesam")]
        [TestCase("senam")]
        public void Should_Not_Return_Anagrams_Shorter_Then_Specified_By_Config(string userInput)
        {
            AnagramSolver anagramSolver = new AnagramSolver(config);
            List<WordModel> anagrams = anagramSolver.GetAnagrams(userInput, wordsRepository.GetWords());
            Assert.IsTrue(anagrams.All(anagram => anagram.word.Length > config.Value.minWordLength));

        }
    }
}
