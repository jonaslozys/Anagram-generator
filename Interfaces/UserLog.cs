using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class UserLog
    {
        public string UserIP { get; set; }
        public string WordSearched { get; set; }

        public UserLog(string userIP, string wordSerched)
        {
            UserIP = userIP;
            WordSearched = wordSerched;
        }
    }
}
