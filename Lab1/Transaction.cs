using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1
{
    public class Transaction
    {
        public TransactionData Data { get; private set; }
        public string Sign { get; private set; }

        public Transaction(string from, string to, int amount, string? privateKey)
        {
            Data = new TransactionData(from, to, amount);
            if (privateKey != null)
            {
                SignTransaction(privateKey);
            }
        }

        private void SignTransaction(string? privateKey)
        {
            var e = new Encrypt();
            Sign = e.SignData(JsonSerializer.Serialize(Data), privateKey);
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }

    public class TransactionData
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public int Amount { get; private set; }

        public TransactionData(string from, string to, int amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"From: {From}; To: {To}; Amount: {Amount}";
        }
    }
}
