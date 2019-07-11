using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramGenerator.Contracts
{
    public class AnagramConfiguration
    {
        public int minWordLength { get; set; }
        public int maxResultsLength { get; set; }

        public AnagramConfiguration(int minWordLength, int maxResultLength)
        {
            this.minWordLength = minWordLength;
            this.maxResultsLength = maxResultLength;
        }

    }
}
