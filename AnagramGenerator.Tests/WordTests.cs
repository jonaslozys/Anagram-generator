﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.Tests
{
    class WordTests
    {
        private WordModel wordObject;
        Dictionary<char, int> letterRegistry;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("medis")]
        [TestCase("qwerty")]
        [TestCase("12345")]
        [TestCase("Kot Letas")]
        public void Should_Create_Dictionary_With_Three_Letters(string testWord)
        {
            letterRegistry = new Dictionary<char, int>();
            foreach (char letter in testWord)
            {
                if (letterRegistry.ContainsKey(letter))
                {
                    letterRegistry[letter] += 1;
                }
                else
                {
                    letterRegistry[letter] = 1;
                }
            }
            wordObject = new WordModel(testWord);
            Assert.AreEqual(wordObject.letterRegistry, letterRegistry);
        }


        [Test]
        [TestCase("medis")]
        [TestCase("qwerty")] 
        [TestCase("12345")]
        [TestCase("Kot Letas")]
        public void Should_Not_Create_Correct_Dictionary(string testWord)
        {
            letterRegistry = new Dictionary<char, int> { { 't', 2 }, { 'r', 1 }, { 's', 2 }, { 'a', 1 } };
            wordObject = new WordModel(testWord);
            Assert.AreNotEqual(wordObject.letterRegistry, letterRegistry);
        }

        [Test]
        [TestCase("medis")]
        [TestCase("qwerty")]
        [TestCase("12345")]
        [TestCase("Kot Letas")]
        public void Should_Correctly_Assign_Word_Property(string testWord)
        {
            wordObject = new WordModel(testWord);
            Assert.AreEqual(wordObject.word, testWord);
        }
    }
}
