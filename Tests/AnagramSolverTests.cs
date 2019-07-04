using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using AnagramLogic;
using System.Configuration;
using System.Linq;
using Tests.dummy_classes;

namespace Tests
{
    class AnagramSolverTests
    {

        private AnagramConfiguration anagramConfiguration;
        private IWordsRepository wordsRepository;

        [SetUp]
        public void SetUp()
        {
            anagramConfiguration = new AnagramConfiguration(5, 15);
            wordsRepository = new DummyWordsRepository();
        }

        [Test]
        [TestCase("menas")]
        [TestCase("nesam")]
        [TestCase("senam")]
        public void Should_Return_Two_Anagrams(string userInput)
        {
            AnagramSolver anagramSolver = new AnagramSolver(wordsRepository, anagramConfiguration);
            List<string> anagrams = anagramSolver.GetAnagrams(userInput);
            Assert.IsTrue(anagrams.Count() == 2);
        }

        [Test]
        [TestCase("menas")]
        [TestCase("nesam")]
        [TestCase("senam")]
        public void Should_Not_Return_Anagrams_Longer_Then_Specified_By_Config(string userInput)
        {
            anagramConfiguration = new AnagramConfiguration(6, 15);
            AnagramSolver anagramSolver = new AnagramSolver(wordsRepository, anagramConfiguration);
            List<string> anagrams = anagramSolver.GetAnagrams(userInput);
            Assert.IsTrue(anagrams.All(anagram => anagram.Length > 6));

        }
    }
}
