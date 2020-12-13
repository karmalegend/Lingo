using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace FillWords
{
    class Program
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bart\Documents\LingoDb.mdf;Integrated Security=True;Connect Timeout=30";

        //https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection?view=dotnet-plat-ext-5.0
        static void Main(string[] args)
        {
            Program program = new Program();
            string line;
            int count = 0;

            string fileName = "rawDutchList.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(Path.Combine(Directory.GetCurrentDirectory(), path));
            while ((line = file.ReadLine()) != null)
            {
                switch (line.Length){
                    case 5:
                        if (new Regex("[/a-z$]{5}").IsMatch(line))
                        {
                            if (program.insertWord("fiveLetterWords", line))
                            {
                                count++;
                            }
                        }
                        break;
                    case 6:
                        if (new Regex("[/a-z$]{6}").IsMatch(line))
                        {
                            if (program.insertWord("sixLetterWords", line))
                            {
                                count++;
                            }
                        }
                        break;
                    case 7:
                        if (new Regex("[/a-z$]{7}").IsMatch(line))
                        {
                            if (program.insertWord("sevenLetterWords", line))
                            {
                                count++;
                            }
                        }
                        break;
                }
            }
            Console.WriteLine($"Succesfully inserted {count} words into the database");
            file.Close();
        }

        private bool insertWord(string table, string word) {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand($"INSERT INTO {table} (word) VALUES ('{word}')", connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

    }
}
