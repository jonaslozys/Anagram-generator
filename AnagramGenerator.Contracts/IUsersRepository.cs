using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public interface IUsersRepository
    {
        void AddUserLog(UserSearchLogModel userLog, string word);
        List<UserSearchLogModel> GetUserLogs(string UserIP);
    }
}
