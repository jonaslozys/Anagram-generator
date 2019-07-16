using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Ef.CodeFirst.Models
{
    public class SearchLog
    {
        public int Id { get; set; }
        public string UserIP { get; set; }
        public DateTime SearchDate { get; set; }
        public string WordSearched { get; set; }
    }
}
