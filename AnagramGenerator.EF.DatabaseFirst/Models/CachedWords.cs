using System;
using System.Collections.Generic;

namespace AnagramGenerator.EF.DatabaseFirst.Models
{
    public partial class CachedWords
    {
        public string Word { get; set; }
        public int Id { get; set; }
    }
}
