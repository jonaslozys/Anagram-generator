using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts.WebAPIResponseModels
{
    public class WordResponseModel
    {
        public string word { get; set; }
        public int Id { get; set; }
        public WordResponseModel(string word, int id)
        {
            this.word = word;
            this.Id = id;
        }
    }
}
