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
            _dbContext.SearchLogs
                .Add(
                    new SearchLog()
                    {
                        SearchDate = userLog.SearchDate,
                        UserIP = userLog.UserIP,
                        WordSearched = wordSearched
                    });

            _dbContext.SaveChanges();
        }

        public List<UserSearchLogModel> GetUserLogs(string userIP)
        {
            List<UserSearchLogModel> searchLogs = _dbContext.SearchLogs
                .Where(l => l.UserIP == userIP)
                .GroupJoin(
                    _dbContext.CachedWords,
                    log => log.WordSearched,
                    cache => cache.Word,
                    (log, cache) => new
                    {
                        SearchId = log.Id,
                        Anagrams = cache,
                        UserIP = log.UserIP,
                        WordSearched = log.WordSearched,
                        SearchDate = log.SearchDate
                    })
                .Select(res => new UserSearchLogModel(res.UserIP, res.WordSearched, res.SearchId) {
                    SearchDate = res.SearchDate, Anagrams = res.Anagrams.Select(c => c.AnagramWord.WordValue).ToList()
                })
                .ToList();

            return searchLogs;
        }

        public void IncreaseUserSearchsCount(string userIP)
        {
            
        }
    }
}
