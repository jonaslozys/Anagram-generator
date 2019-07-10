using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.configurations
{
    public class AnagramConfiguration
    {
        public int minWordLength { get; set; }
        public int maxResultsLength { get; set; }

        public AnagramConfiguration()
        {

        }
    }
}
