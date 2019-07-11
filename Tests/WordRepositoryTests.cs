using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using AnagramGenerator.BusinessLogic;
using System.Linq;

namespace Tests
{
    class WordRepositoryTests
    {
        private WordsRepository wordsRepository;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Return_Not_Empy_Hashset()
        {
            wordsRepository = new WordsRepository();
            Assert.IsTrue(wordsRepository.GetWords().GetType() == typeof(HashSet<WordModel>) && wordsRepository.GetWords().Count > 0);
        }

        [Test]
        [TestCase(new object[] { "pakeleivingas", "pakeleivingos", "pakeleivingos" })]
        [TestCase(new object[] { "žvilgsnis", "seminaras", "Semionovičiumi" })]
        public void Shoud_Correctly_Parse_All_Unique_Words_From_Input_File(object[] inputWords)
        {
            wordsRepository = new WordsRepository();
            Assert.IsTrue(inputWords.All(inputWord => wordsRepository.GetWords().Any(word => word.word == (string)inputWord)));
        }

    }
}
