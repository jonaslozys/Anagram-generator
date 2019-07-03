using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Tests
{
    class WordTest1
    {
        private string word;
        private Word wordObject;
        Dictionary<char, int> letterRegistry;

        [SetUp]
        public void SetupTest1()
        {
            word = "testas";
            letterRegistry = new Dictionary<char, int> { { 't', 2 }, { 'e', 1 }, { 's', 2 }, { 'a', 1 } };
        }

        [Test]
        public void Should_Create_Dictionary_With_Three_Letters()
        {
            wordObject = new Word(word);
            Assert.AreEqual(wordObject.letterRegistry, this.letterRegistry);
        }
    }

    class WordTest2
    {
        private string word;
        private Word wordObject;
        Dictionary<char, int> letterRegistry;

        [SetUp]
        public void SetupTest2()
        {
            word = "testas";
            letterRegistry = new Dictionary<char, int> { { 't', 2 }, { 'r', 1 }, { 's', 2 }, { 'a', 1 } };
        }

        [Test]
        public void Should_Not_Create_Correct_Dictionary()
        {
            wordObject = new Word(word);
            Assert.AreNotEqual(wordObject.letterRegistry, this.letterRegistry);
        }
    }
}
