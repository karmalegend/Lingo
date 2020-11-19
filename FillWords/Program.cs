using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FillWords
{
    class Program
    {
        static void Main(string[] args)
        {
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
                            System.Console.WriteLine(line);
                            count++;
                        }
                        break;
                    case 6:
                        if (new Regex("[/a-z$]{6}").IsMatch(line))
                        {
                            System.Console.WriteLine(line);
                            count++;
                        }
                        break;
                    case 7:
                        if (new Regex("[/a-z$]{7}").IsMatch(line))
                        {
                            System.Console.WriteLine(line);
                            count++;
                        }
                        break;
                }
                
            }

            Console.WriteLine($"valid words {count}");

            file.Close();
        }
    }
}
