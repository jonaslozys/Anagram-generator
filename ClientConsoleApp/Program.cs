using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClientConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task SendRequest(string word)
        {
            try
            {
                Uri uri = new Uri($"https://localhost:5001/api/anagrams/{word}");
                Console.WriteLine($"Sending request to {uri}");
                HttpResponseMessage response = await client.GetAsync(uri);
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response.ToString());
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e);
            }

        }

        static void Main(string[] args)
        {
            Program program = new Program();

            string userInputString = "";

            foreach (string word in args)
            {
                userInputString += word;
            }

            program.SendRequest(userInputString).Wait();

        }
    }
}
