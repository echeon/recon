using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace recon
{
    public class Account
    {
        public List<Stock> Portfolio { get; set; }
        public double Balance { get; set;}

        public Account()
        {
            Portfolio = new List<Stock>();
            Balance = 0;
        }

        public void SetupAccount(string line)
        {
            string[] data = line.Split(" ");

            string symbol = data[0];
            double value = double.Parse(data[1]);

            if (symbol == "Cash") {
                Balance = value;
            }
            else
            {
                Portfolio.Add(new Stock(symbol, value));
            }
        }

        public void HandleTransaction(string transaction)
        {
            string[] data = transaction.Split(' ');
            string symbol = data[0];
            string type = data[1];
            double numShares = double.Parse(data[2]);
            double totalValue = double.Parse(data[3]);

            if (symbol == "Cash")
            {
                HandleCashTransaction(type, totalValue);
            }
            else
            {
                HandleStockTransaction(symbol, type, numShares, totalValue);
            }
        }

        public void Reconciliate(Account account, string outputPath)
        {
            var result = new Dictionary<string, double>();

            // reconciliate cash balance first
            result["Cash"] = account.Balance - Balance;
                
            foreach(var stock in account.Portfolio)
            {
                result[stock.Symbol] = stock.NumShares;
            }

            foreach(var stock in Portfolio)
            {
                if (result.ContainsKey(stock.Symbol))
                {
                    result[stock.Symbol] -= stock.NumShares;
                }
                else
                {
                    result[stock.Symbol] = -stock.NumShares;
                }
            }

            WriteFile(result, outputPath);
        }
            

        private void WriteFile(Dictionary<string, double> result, string outputPath)
        {
            using (StreamWriter file = new StreamWriter(outputPath))
            {
                foreach (var r in result)
                {
                    if (r.Value == 0)
                        continue;

                    file.WriteLine("{0} {1}", r.Key, r.Value);
                    Console.WriteLine("{0} {1}", r.Key, r.Value);
                }
            }
        }

        private void HandleStockTransaction(string symbol, string type, double numShares, double totalValue)
        {
            var stock = Portfolio.FirstOrDefault(s => s.Symbol == symbol);
            
            // create and add stock to portfolio if not found
            if (stock == null)
            {
                stock = new Stock(symbol, 0);
                Portfolio.Add(stock);
            }

            if (type == "BUY")
            {
                Withdraw(totalValue);
                stock.Buy(numShares);
            }
            else if (type == "SELL" || type == "DIVIDEND")
            {
                stock.Sell(numShares);
                Deposit(totalValue);
            }
        }

        private void HandleCashTransaction(string type, double amount)
        {
            if (type == "DEPOSIT")
            {
                Deposit(amount);
            }
            else if (type == "FEE")
            {
                Withdraw(amount);
            }
        }

        private void Withdraw(double amount)
        {
            Balance -= amount;
        }

        private void Deposit(double amount)
        {
            Balance += amount;
        }

    }
}