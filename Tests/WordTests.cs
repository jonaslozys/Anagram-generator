using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Tests
{
    class WordTests
    {
        private Word wordObject;
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
            wordObject = new Word(testWord);
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
            wordObject = new Word(testWord);
            Assert.AreNotEqual(wordObject.letterRegistry, letterRegistry);
        }

        [Test]
        [TestCase("medis")]
        [TestCase("qwerty")]
        [TestCase("12345")]
        [TestCase("Kot Letas")]
        public void Should_Correctly_Assign_Word_Property(string testWord)
        {
            wordObject = new Word(testWord);
            Assert.AreEqual(wordObject.word, testWord);
        }

        static Dictionary<char, int> DummyDictionary(string word)
        {
            Dictionary<char, int> dummyDictionary = new Dictionary<char, int>();
            foreach (char letter in word)
            {
                if (dummyDictionary.ContainsKey(letter))
                {
                    dummyDictionary[letter] += 1;
                }
                else
                {
                    dummyDictionary[letter] = 1;
                }
            }

            return dummyDictionary;
        }
    }
}
