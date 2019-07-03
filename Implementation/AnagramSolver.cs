using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Interfaces;
using System.Linq;

namespace Implementation
{
    public class AnagramSolver : IAnagramSolver
    {
        IWordsRepository wordsRepository;

        private HashSet<Word> words;

        private Word userInput;

        private List<string> anagrams;
        public AnagramSolver(IWordsRepository wordsRepository, string userInput)
        {
            this.wordsRepository = wordsRepository;
            this.anagrams = new List<string>();
            this.words = wordsRepository.GetLines();
            this.userInput = new Word(userInput);
        }

        private bool CompareWords(Word word1, Word word2)
        {
            if (word1.letterRegistry.Count != word2.letterRegistry.Count)
            {
                return false;
            } else
            {
                return word1.letterRegistry.Keys.All(key => {
                    if(word2.letterRegistry.ContainsKey(key))
                    {
                        return word2.letterRegistry[key] == word1.letterRegistry[key];

                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        private void FindSingleWordAnagrams()
        {
            foreach(Word word in words)
            {
                if(CompareWords(userInput, word))
                {
                    if(userInput.word != word.word)
                    {
                        anagrams.Add(word.word);
                    }
                }
            }
        }

        private bool CompareSubdictionaries(Word userInput, Word word)
        {
            if (userInput.letterRegistry.Count < word.letterRegistry.Count)
            {
                return false;
            }
            else
            {
                return word.letterRegistry.Keys.All(key => {
                    if (userInput.letterRegistry.ContainsKey(key))
                    {
                        return userInput.letterRegistry[key] == word.letterRegistry[key];

                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        private void FindMultipleWordAnagrams()
        {
            Word userInputCopy = userInput;
            string anagram = "";

            Console.WriteLine(userInputCopy.letterRegistry.Count);
            foreach (Word word in words)
            {
                if(CompareSubdictionaries(userInputCopy, word) && word.word.Length > 1)
                {
                    anagram += $" {word.word}";
                    Console.WriteLine(anagram);

                    foreach (KeyValuePair<char, int> letter in word.letterRegistry)
                    {
                        if(userInputCopy.letterRegistry[letter.Key] < 1)
                        {
                            userInputCopy.letterRegistry.Remove(letter.Key);
                        } else
                        {
                            userInputCopy.letterRegistry[letter.Key] -= letter.Value;
                            if (userInputCopy.letterRegistry[letter.Key] < 1)
                            {
                                userInputCopy.letterRegistry.Remove(letter.Key);
                            }


                        }
                    }
                }

                if(userInputCopy.letterRegistry.Count < 1 && anagram.Length > 0)
                {
                    anagrams.Add(anagram);
                    anagram = "";
                    userInputCopy = userInput;
                }
            }
        }
        public List<string> GetAnagrams()
        {
            this.FindSingleWordAnagrams();
            this.FindMultipleWordAnagrams();

            return this.anagrams;
        }

    }
}
