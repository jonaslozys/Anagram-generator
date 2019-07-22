using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;
using AnagramGenerator.BusinessLogic;
using System.Linq;
using Microsoft.Extensions.Options;
using AnagramGenerator.Contracts.configurations;

namespace Tests
{
    class WordRepositoryTests
    {
        private WordsRepository wordsRepository;
        private IOptions<Connection> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create<Connection>(new Connection() { ConnectionString = "Data Source=LT-LIT-SC-0166;Initial Catalog=Anagrams;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" });
        }

        [Test]
        public void Should_Return_Not_Empy_Hashset()
        {
            wordsRepository = new WordsRepository(config);
            Assert.IsTrue(wordsRepository.GetWords().GetType() == typeof(HashSet<WordModel>) && wordsRepository.GetWords().Count > 0);
        }

        [Test]
        [TestCase(new object[] { "pakeleivingas", "pakeleivingos", "pakeleivingos" })]
        [TestCase(new object[] { "žvilgsnis", "seminaras"})]
        public void Shoud_Correctly_Parse_All_Unique_Words_From_Input_File(object[] inputWords)
        {
            wordsRepository = new WordsRepository(config);
            Assert.IsTrue(inputWords.All(inputWord => wordsRepository.GetWords().Any(word => word.word == (string)inputWord)));
        }

    }
}
