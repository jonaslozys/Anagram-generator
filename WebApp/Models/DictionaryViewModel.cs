using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;

namespace WebApp.Models
{
    public class DictionaryViewModel
    {
        public List<WordModel> wordsDictionary { get; set; }
        public string SearchString { get; set; }
        public int page { get; set; }

    }
}
