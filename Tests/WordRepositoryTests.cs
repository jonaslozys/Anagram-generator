using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using AnagramLogic;

namespace Tests
{
    class WordRepositoryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Return_Not_Empy_Hashset()
        {
            WordsRepository wordsRepository = new WordsRepository();

            Assert.IsTrue(wordsRepository.GetWords().GetType() == typeof(HashSet<Word>) && wordsRepository.GetWords().Count > 0);

        }

    }
}
