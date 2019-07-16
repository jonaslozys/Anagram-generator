using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnagramGenerator.EF.DatabaseFirst.Models;
using AnagramGenerator.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AnagramGenerator.EF.DatabaseFirst
{
    
    public class EfUsersRepository
    {
        private AnagramsContext _dbContext;

        public EfUsersRepository(AnagramsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUserLog(UserLog userLog, string word)
        {
            Words wordSearched = _dbContext.Words.FirstOrDefault(w => w.Word == word);
            _dbContext.UserLog.Add(userLog);
            _dbContext.SaveChanges();
        }

        public List<UserSearchLogModel> GetUserLogs(string userIP)
        {
            List<UserSearchLogModel> historyLogs = _dbContext.UserLog.Select(x => new UserSearchLogModel(x.UserIp, x.WordSearched, x.Id)
            {
                Anagrams = _dbContext.CachedWords
                    .Where(c => c.Word == x.WordSearched)
                    .Select(c => c.IdNavigation.Word).ToList(),
                SearchDate = x.SearchDate
            })
                .Where(x => x.UserIP == userIP)
                .ToList();

            return historyLogs;
        }
    }
}
