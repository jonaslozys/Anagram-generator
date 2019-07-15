using System;
using System.Data.SqlClient;
using AnagramGenerator.Contracts;
using AnagramGenerator.BusinessLogic;
using System.Collections.Generic;
using System.Threading;

namespace DatabaseFiller
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=LT-LIT-SC-0166;Initial Catalog=AnagramsCodeFirst;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            IFileRepository fileRepository = new FileRepository();
            HashSet<WordModel> words = fileRepository.GetWords();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (WordModel word in words)
                {
                    if (word.word.Contains("'"))
                    {
                        int queteIndex = word.word.IndexOf("'");
                        word.word = word.word.Insert(queteIndex, @"'");
                    }
                    string query = $"INSERT INTO Words (WordValue) VALUES (\'{word.word}\');";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.ExecuteScalar();
                    }
                }
            }


        }
    }
}
