using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUsersRepository
    {
        void AddUserLog(UserSearchLogModel userLog);
        List<UserSearchLogModel> GetUserLogs(string UserIP);
    }
}
