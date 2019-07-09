using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramLogic;
using Contracts;
using System.Linq;
using System.Data.SqlClient;

namespace AnagramLogic
{
    public class WordsRepository : IWordsRepository
    {
        private HashSet<Word> _wordList;
        private string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";
        private string _connectionString;

        public WordsRepository()
        {

        }

        public WordsRepository(string connectionString)
        {
            _connectionString = connectionString;
            _wordList = new HashSet<Word>();
        }
        public HashSet<Word> GetWords()
        {
            if (_wordList.Count > 1)
            {
                return _wordList;

            } else
            {
                _wordList = new HashSet<Word>();

                string query = "SELECT Word FROM Words;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        _wordList.Add(new Word(reader.GetString(0)));
                    }

                    reader.Close();

                }

                return _wordList;
            }
        }

        public List<string> GetPageOfWords(int pageSize, int pageNumber)
        {
            List<string> pageOfWords = this.GetWords()
                .Select(word => word.word)
                .ToList();
               
            int index = (pageNumber - 1) * pageSize;
            int count = pageSize;

            if (index < 0) index = 0;
            if((index + pageSize) > pageOfWords.Count )
            {
                index = pageOfWords.Count - pageSize;
                count = pageSize;
            }

            pageOfWords = pageOfWords
                .GetRange(index, count)
                .ToList();

            return pageOfWords;
        }

        public byte[] GetDictionaryFile()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes($@"{target}/zodynas.txt");
            return fileBytes;

        }

        public List<string> GetSearchedWords(string searchString)
        {
            List<string> searchResults = this.GetWords()
                .Select(word => word.word)
                .ToList();
            
            if (searchResults != null)
            {
                searchResults = searchResults.Where(s => s.Contains(searchString)).ToList();
            }

            return searchResults;
        }

        public List<string> GetCachedAnagrams(string word)
        {
            List<string> anagrams = new List<string>();

            List<int> anagramIndexes = new List<int>();

            string query = $"SELECT * FROM CachedWords WHERE Word = '{word}';";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    anagramIndexes.Add(reader.GetInt32(1));
                }

                if (anagramIndexes.Count > 0)
                {
                    foreach (int index in anagramIndexes)
                    {
                        query = $"SELECT * FROM Words WHERE Id = '{index}'";
                        command = new SqlCommand(query, connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            anagrams.Add(reader.GetString(0));
                        }
                    }
                }

                reader.Close();

            }


            return anagrams;

        } 

    }
}
