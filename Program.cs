using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace recon
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "recon.in");
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "recon.out");

            if (args.Length >= 2)
            {
                filePath = $@"{args[0]}";
                outputPath = $@"{args[1]}";
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"No file found at the specified path: {filePath}");
                return;
            }

            Account account1 = new Account();
            Account account2 = new Account();

            string[] lines = System.IO.File.ReadAllLines(filePath);

            string section = null;

            foreach (string line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                    continue;

                if (line == "D0-POS" || line == "D1-TRN" || line == "D1-POS")
                {
                    section = line;
                }
                else
                {
                    if (section == "D0-POS")
                    {
                        account1.SetupAccount(line);
                    }
                    else if (section == "D1-TRN")
                    {
                        account1.HandleTransaction(line);
                    }
                    else if (section == "D1-POS")
                    {
                        account2.SetupAccount(line);
                    }
                }
            }

            account1.Reconciliate(account2, outputPath);
        }
    }
}
