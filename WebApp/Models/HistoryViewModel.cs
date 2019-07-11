using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;

namespace WebApp.Models
{
    public class HistoryViewModel
    {
        public List<string> SearchHistory {get;set;}
        public List<UserSearchLogModel> HistoryLogs { get; set; }
    }
}
