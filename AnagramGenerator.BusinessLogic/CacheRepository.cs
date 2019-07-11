using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramGenerator.BusinessLogic;
using AnagramGenerator.Contracts;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using AnagramGenerator.Contracts.configurations;

namespace AnagramGenerator.BusinessLogic
{
    public class CacheRepository : ICacheRepository
    {
        private Connection _connection;
        private string _connectionString;

        public CacheRepository(IOptionsMonitor<Connection> optionsAccessor)
        {
            _connection = optionsAccessor.CurrentValue;
            _connectionString = _connection.ConnectionString;
        }


        public List<WordModel> GetCachedAnagrams(string word)
        {
            List<WordModel> anagrams = new List<WordModel>();

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

                reader.Close();

                if (anagramIndexes.Count > 0)
                {
                    foreach (int index in anagramIndexes)
                    {
                        query = $"SELECT * FROM Words WHERE Id = '{index}'";
                        command = new SqlCommand(query, connection);
                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            anagrams.Add(new WordModel(reader.GetString(1)));
                        }

                        reader.Close();
                    }
                }

                reader.Close();

            }

            return anagrams;

        }

        public void UpdateAnagramsCache(string word, List<WordModel> anagrams)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (WordModel anagram in anagrams)
                {
                    string query = $"INSERT INTO CachedWords VALUES ('{word}', '{anagram.Id}');";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.ExecuteNonQuery();
                }

            }
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

    }
}
