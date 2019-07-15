using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public class UserSearchLogModel
    {
        public int? SeachId { get; set; }
        public string UserIP { get; set; }
        public string WordSearched { get; set; }
        public List<string> Anagrams { get; set; }
        public DateTime SearchDate { get; set; }
        public UserSearchLogModel(string userIP, string wordSerched, int? searchId)
        {
            UserIP = userIP;
            WordSearched = wordSerched;
            Anagrams = new List<string>();
            if(searchId != null)
            {
                SeachId = searchId;
            }
        }
    }
}
