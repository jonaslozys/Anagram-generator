using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Contracts;
using System.Data;
using System.Linq;

namespace AnagramLogic
{
    public class UsersRepository : IUsersRepository
    {
        private string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUserLog(UserLog userLog)
        {
            string query = "INSERT INTO UserLog (UserIP, WordSearched) VALUES (@UserIP, @WordSearched);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                           "RIGHT JOIN Words ON(Words.Id = CachedWords.Id) " +
                           "WHERE UserLog.UserIP = @UserIP";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                command.Parameters.Add("@UserIp", SqlDbType.NVarChar);
                command.Parameters["@UserIP"].Value = userIP;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int searchId = reader.GetInt32(0);
                    string anagram = reader.GetString(3);

                    if (userLogs.Where(log => log.SeachId == searchId).Count() > 0)
                    {
                        userLogs.Single(log => log.SeachId == searchId).Anagrams.Add(anagram);
                    }
                    else
                    {
                        string userIp = reader.GetString(1);
                        string wordSearcged = reader.GetString(2);
                        UserLog userLog = new UserLog(userIP, wordSearcged, searchId);
                        userLog.Anagrams.Add(anagram);
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
