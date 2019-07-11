using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;

namespace WebApp.Models
{
    public class AnagramsViewModel
    {
        public List<string> Anagrams { get; set; }

        public string Word { get; set; }
    }
}
