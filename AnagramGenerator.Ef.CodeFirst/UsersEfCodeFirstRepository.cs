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

        public List<UserSearchLogModel> GetUserLogs(string userIP)
        {
            List<UserSearchLogModel> historyLogs = _dbContext.UserLogs.Select(x => new UserSearchLogModel(x.UserIP, x.WordSearched, x.Id)
            {
                    Anagrams = _dbContext.CachedWords.Where(c => c.Word == x.WordSearched).Select(c => c.AnagramWord.WordValue).ToList(),
                    SearchDate = x.SearchDate
                })
            .Where(x => x.UserIP == userIP)
            .ToList();

            return historyLogs;
        }
    }
}
