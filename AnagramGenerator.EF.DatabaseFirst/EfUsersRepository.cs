using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.EF.DatabaseFirst.Models;
using AnagramGenerator.Contracts;

namespace AnagramGenerator.EF.DatabaseFirst
{
    
    public class EfUsersRepository
    {
        private AnagramsContext _dbContext;

        public EfUsersRepository(AnagramsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserLog(UserLog userLog)
        {
            _dbContext.UserLog.Add(userLog);
            _dbContext.SaveChanges();
        }

        public List<UserSearchLogModel> GetUserLogs(string userIP)
        {
            List<UserSearchLogModel> historyLogs = new List<UserSearchLogModel>();

            var query = from logs in _dbContext.UserLog
                        where logs.UserIp == userIP
                        from cachedWords in _dbContext.CachedWords
                            .Where(cachedWord => cachedWord.Word == logs.WordSearched).DefaultIfEmpty()
                        from words in _dbContext.Words
                            .Where(word => word.Id == cachedWords.Id).DefaultIfEmpty()
                         select new
                         {
                             Id = logs.Id,
                             UserIp = logs.UserIp,
                             WordSearched = logs.WordSearched,
                             Anagram = words.Word,
                             SearchDate = logs.SearchDate
                         };

            foreach (var entry in query)
            {
                int searchId = entry.Id;
                string anagram = entry.Anagram;

                if (historyLogs.Where(log => log.SeachId == searchId).Count() > 0)
                {
                    historyLogs.Single(log => log.SeachId == searchId).Anagrams.Add(anagram);
                }
                else
                {
                    UserSearchLogModel historyLog = new UserSearchLogModel(entry.UserIp, entry.WordSearched, searchId);
                    historyLog.Anagrams.Add(entry.Anagram);
                    historyLog.SeachDate = entry.SearchDate;
                    historyLogs.Add(historyLog);
                }
            }

            return historyLogs;
        }
    }
}
