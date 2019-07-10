using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Contracts;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Options;
using Contracts.configurations;

namespace AnagramLogic
{
    public class UsersRepository : IUsersRepository
    {
        private Connection _connection;

        public UsersRepository(IOptionsMonitor<Connection> optionsAccessor)
        {
            _connection = optionsAccessor.CurrentValue;
        }

        public void AddUserLog(UserLog userLog)
        {
            string query = "INSERT INTO UserLog (UserIP, WordSearched) VALUES (@UserIP, @WordSearched);";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                command.Parameters.Add("@UserIp", SqlDbType.NVarChar);
                command.Parameters["@UserIP"].Value = userLog.UserIP;

                command.Parameters.Add("@WordSearched", SqlDbType.NVarChar);
                command.Parameters["@WordSearched"].Value = userLog.WordSearched;

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<UserLog> GetUserLogs(string userIP)
        {
            List<UserLog> userLogs = new List<UserLog>();

            string query = "SELECT UserLog.Id, UserIP, UserLog.WordSearched, UserLog.SearchDate, Words.Word AS 'Anagram' " +
                           "FROM UserLog " +
                           "LEFT JOIN CachedWords ON(CachedWords.Word = UserLog.WordSearched) " +
                           "LEFT JOIN Words ON(Words.Id = CachedWords.Id) " +
                           "WHERE UserLog.UserIP = @UserIP";

            using (SqlConnection connection = new SqlConnection(_connection.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                command.Parameters.Add("@UserIp", SqlDbType.NVarChar);
                command.Parameters["@UserIP"].Value = userIP;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int searchId = reader.GetInt32(0);
                    string anagram = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";

                    if (userLogs.Where(log => log.SeachId == searchId).Count() > 0)
                    {
                        userLogs.Single(log => log.SeachId == searchId).Anagrams.Add(anagram);
                    }
                    else
                    {
                        string userIp = reader.GetString(1);
                        string wordSearched = reader.GetString(2);
                        DateTime searchDate = reader.GetDateTime(3);
                        UserLog userLog = new UserLog(userIP, wordSearched, searchId);
                        userLog.Anagrams.Add(anagram);
                        userLog.SeachDate = searchDate;
                        userLogs.Add(userLog);
                    }
                }

                reader.Close();

                connection.Close();
            }

            return userLogs;
        }
    }
}
