using System;
using Implementation;

namespace Implementation
{
    public static class FileReader
    {
        public static void ReadFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\jonas\Desktop\tasks\Anagram generator\zodynas.txt");

            foreach (string line in lines)
            {
                Console.WriteLine(line);
                WordsRepository wordsRepository = new WordsRepository();
                wordsRepository.AddWord(line);
            }
        }
    }
}
