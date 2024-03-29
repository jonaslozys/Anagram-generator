﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using AnagramGenerator.Contracts;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Options;
using AnagramGenerator.Contracts.configurations;

namespace AnagramGenerator.BusinessLogic
{
    public class UsersRepository : IUsersRepository
    {
        private Connection _connection;

        public UsersRepository(IOptions<Connection> optionsAccessor)
        {
            _connection = optionsAccessor.Value;
        }

        public void AddUserLog(UserSearchLogModel userLog, string word)
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

        public void DecreaseAvailabeUserSearches(string UserIP)
        {
            throw new NotImplementedException();
        }

        public List<UserSearchLogModel> GetUserLogs(string userIP)
        {
            List<UserSearchLogModel> userLogs = new List<UserSearchLogModel>();

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
                        UserSearchLogModel userLog = new UserSearchLogModel(userIP, wordSearched, searchId);
                        userLog.Anagrams.Add(anagram);
                        userLog.SearchDate = searchDate;
                        userLogs.Add(userLog);
                    }
                }

                reader.Close();

                connection.Close();
            }

            return userLogs;
        }

        public void IncreaseAvailabeUserSearches(string UserIP)
        {
            throw new NotImplementedException();
        }
    }
}
