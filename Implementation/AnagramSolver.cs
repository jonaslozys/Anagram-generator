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

        private void FindAnagrams()
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
        public List<string> GetAnagrams()
        {
            this.FindAnagrams();

            return this.anagrams;
        }

    }
}
