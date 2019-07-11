using System;
using System.Collections.Generic;

namespace Anagram_Generator.EF.DatabaseFirst.Models
{
    public partial class UserLog
    {
        public int Id { get; set; }
        public string UserIp { get; set; }
        public string WordSearched { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
