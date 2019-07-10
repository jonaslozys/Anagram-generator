using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUsersRepository
    {
        void AddUserSearch(UserLog userLog);
        List<UserLog> GetUserLogs(string UserIP);
    }
}
