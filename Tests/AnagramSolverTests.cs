using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using AnagramLogic;
using System.Configuration;
using System.Linq;

namespace Tests
{
    class AnagramSolverTests
    {

        private string userInput = "";
        private AnagramConfiguration anagramConfiguration;
        private IWordsRepository wordsRepository;

        [SetUp]
        public void SetUp()
        {
            userInput = "menas";
            anagramConfiguration = new AnagramConfiguration(5, 15);
            wordsRepository = new WordsRepository();
        }

        [Test]
        public void Should_Return_True_Two_Anagrams()
        {
            AnagramSolver anagramSolver = new AnagramSolver(wordsRepository, anagramConfiguration);
            List<string> anagrams = anagramSolver.GetAnagrams(userInput);
            Assert.IsTrue(anagrams.Count() == 2);
        }

        [Test]
        public void Do_Not_Return_Anagrams_Longer_Then_Specified_By_Config()
        {
            anagramConfiguration = new AnagramConfiguration(6, 15);
            AnagramSolver anagramSolver = new AnagramSolver(wordsRepository, anagramConfiguration);
            List<string> anagrams = anagramSolver.GetAnagrams(userInput);
            Assert.IsTrue(anagrams.All(anagram => anagram.Length > 6));

        }
    }
}
