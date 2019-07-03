using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Tests
{
    class WordTests
    {
        private string word;
        private Word wordObject;
        Dictionary<char, int> letterRegistry;

        [SetUp]
        public void Setup()
        {
            word = "testas";
        }

        [Test]
        public void Should_Create_Dictionary_With_Three_Letters()
        {
            letterRegistry = new Dictionary<char, int> { { 't', 2 }, { 'e', 1 }, { 's', 2 }, { 'a', 1 } };
            wordObject = new Word(word);
            Assert.AreEqual(wordObject.letterRegistry, this.letterRegistry);
        }


        [Test]
        public void Should_Not_Create_Correct_Dictionary()
        {
            letterRegistry = new Dictionary<char, int> { { 't', 2 }, { 'r', 1 }, { 's', 2 }, { 'a', 1 } };
            wordObject = new Word(word);
            Assert.AreNotEqual(wordObject.letterRegistry, this.letterRegistry);
        }

        [Test]
        public void Should_Correctly_Assign_Word_Property()
        {
            wordObject = new Word(word);
            Assert.AreEqual(wordObject.word, word);
        }
    }
}
