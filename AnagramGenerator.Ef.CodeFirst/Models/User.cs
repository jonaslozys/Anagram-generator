using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Ef.CodeFirst.Models
{
    public class User
    {
        public string UserIP { get; set; }
        public int AvailableSearches { get; set; }
    }
}
