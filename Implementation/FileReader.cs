using System;
using System.IO;
using Implementation;

namespace Implementation
{
    public static class FileReader
    {
        public static void ReadFile()
        {
            string target = @"C:\Users\jonas\Desktop\tasks\Anagram Generator";

            Environment.CurrentDirectory = target;

            WordsRepository wordsRepository = new WordsRepository();

            using (StreamReader sr = new StreamReader($@"{target}/zodynas.txt"))
            {
                string line;
                string[] words;


                while((line = sr.ReadLine().Trim()) != null)
                {
                    //Console.WriteLine(line);
                    words = line.Split(new[] { '\t' });
                    wordsRepository.AddWord(words[0]);
                    wordsRepository.AddWord(words[2]);

                }
            }
        }
    }
}
