using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Ef.CodeFirst.Models
{
    public class UserLog
    {
        public int Id { get; set; }
        public string UserIP { get; set; }
        public DateTime SearchDate { get; set; }
        public Word WordSearched { get; set; }
    }
}
