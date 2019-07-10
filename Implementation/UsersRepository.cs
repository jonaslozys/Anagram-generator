using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Contracts;
using System.Data;

namespace AnagramLogic
{
    public class UsersRepository : IUsersRepository
    {
        private string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUserSearch(UserLog userLog)
        {
            string query = "INSERT INTO UserLog VALUES (@UserIP, @WordSearched);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                command.Parameters.Add("@UserIp", SqlDbType.NVarChar);
                command.Parameters["@UserIP"].Value = userLog.UserIP;

                command.Parameters.Add("@WordSearched", SqlDbType.NVarChar);
                command.Parameters["@UserIP"].Value = userLog.UserIP;

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public List<UserLog> GetUserLogs(string UserIP)
        {
            throw new NotImplementedException();
        }
    }
}
