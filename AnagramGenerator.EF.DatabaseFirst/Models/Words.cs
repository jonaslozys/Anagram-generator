using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class Words
    {
        public int Id { get; set; }
        public string Word { get; set; }
    }
}
