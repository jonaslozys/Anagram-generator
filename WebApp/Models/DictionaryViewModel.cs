using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DictionaryViewModel
    {
        public List<string> wordsDictionary { get; set; }
        public string SearchString { get; set; }
        public int page { get; set; }

    }
}
