using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnagramGenerator.Ef.CodeFirst.Models;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.Ef.CodeFirst
{
    public class UsersEfCodeFirstRepository : IUsersRepository
    {
        private AnagramContext _dbContext;

        public UsersEfCodeFirstRepository(AnagramContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserLog(UserSearchLogModel userLog, string wordSearched)
        {
            _dbContext.UserLogs
                .Add(
                    new UserLog()
                    {
                        SearchDate = userLog.SearchDate,
                        UserIP = userLog.UserIP,
                        WordSearched = wordSearched
                    });

            _dbContext.SaveChanges();
        }

        public List<UserSearchLogModel> GetUserLogs(string UserIP)
        {
            throw new NotImplementedException();
        }
    }
}
