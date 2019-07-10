using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class UserLog
    {
        public string UserIP { get; set; }
        public string WordSearched { get; set; }
        public DateTime SearchTime { get; set; }
    }
}
