using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramGenerator.Contracts.WebAPIResponseModels;

namespace WebApp.Models
{
    public class AnagramsViewModel
    {
        public List<WordResponseModel> Anagrams { get; set; }
        public string Word { get; set; }
        public string ErrorMessage { get; set; }
    }
}
