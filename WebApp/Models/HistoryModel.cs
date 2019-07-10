using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;

namespace WebApp.Models
{
    public class HistoryModel
    {
        public List<string> SearchHistory {get;set;}
        public List<UserLog> HistoryLogs { get; set; }
    }
}
