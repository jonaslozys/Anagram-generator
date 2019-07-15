using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnagramGenerator.Ef.CodeFirst.Models
{
    public class Word
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string WordValue {get;set;}
    }
}
