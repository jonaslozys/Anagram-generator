using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class UserLog
    {
        public int? SeachId { get; set; }
        public string UserIP { get; set; }
        public string WordSearched { get; set; }
        public List<string> Anagrams { get; set; }

        public UserLog(string userIP, string wordSerched, int? searchId)
        {
            UserIP = userIP;
            WordSearched = wordSerched;
            if(searchId != null)
            {
                SeachId = searchId;
            }
        }
    }
}
