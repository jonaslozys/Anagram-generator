using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Ef.CodeFirst.Models
{
    public class CachedWord
    {
        public string Word { get; set; }
        public Word AnagramWord { get; set; }
    }
}
