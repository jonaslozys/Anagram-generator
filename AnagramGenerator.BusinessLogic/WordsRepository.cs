using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramGenerator.Contracts;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using AnagramGenerator.Contracts.configurations;

namespace AnagramGenerator.BusinessLogic
{
    public class WordsRepository : IWordsRepository
    {
        private Connection _connection;
        private HashSet<WordModel> _wordList;
        private string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";
        private string _connectionString;

        public WordsRepository()
        {

        }

        public WordsRepository(IOptionsMonitor<Connection> optionsAccessor)
        {
            _connection = optionsAccessor.CurrentValue;
            _connectionString = _connection.ConnectionString;
            _wordList = new HashSet<WordModel>();
        }
        public HashSet<WordModel> GetWords()
        {
            if (_wordList.Count > 1)
            {
                return _wordList;

            } else
            {
                _wordList = new HashSet<WordModel>();

                string query = "SELECT Word, Id FROM Words;";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        _wordList.Add(new WordModel(reader.GetString(0), reader.GetInt32(1)));
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

        public List<string> GetSearchedWords(string searchString)
        {
            List<string> searchResults = new List<string>();

            string query = $"SELECT Word FROM Words WHERE Word LIKE '{searchString}%';";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    searchResults.Add(reader.GetString(0));
                }

                reader.Close();

            }

            return searchResults;
        }

        public void DeleteWord(string word)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("deleteWord", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("Word", word));

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void AddNewWord(string word)
        {
            throw new NotImplementedException();
        }
    }
}
