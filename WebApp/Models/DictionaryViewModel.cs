using System;
using System.Collections.Generic;
using AnagramGenerator.Contracts.WebAPIResponseModels;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts;

namespace WebApp.Models
{
    public class DictionaryViewModel
    {
        public List<WordResponseModel> wordsDictionary { get; set; }
        public string SearchString { get; set; }
        public int page { get; set; }
        public string ErrorMessage { get; set; }

    }
}
