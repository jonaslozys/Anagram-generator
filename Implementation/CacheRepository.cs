using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using AnagramLogic;
using Contracts;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Options;
using Contracts.configurations;

namespace AnagramLogic
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
                            anagrams.Add(reader.GetString(1));
                        }

                        reader.Close();
                    }
                }

                reader.Close();

            }

            return anagrams;

        }

        public void UpdateAnagramsCache(string word, List<string> anagrams)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (string anagram in anagrams)
                {
                    string query = $"SELECT Id FROM Words WHERE Word = '{anagram}'";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    int? anagramId = null;

                    while (reader.Read())
                    {
                        anagramId = reader.GetInt32(0);
                    }

                    reader.Close();

                    if (anagramId != null)
                    {
                        query = $"INSERT INTO CachedWords VALUES ('{word}', '{anagramId}');";

                        command = new SqlCommand(query, connection);

                        command.ExecuteNonQuery();

                    }
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
