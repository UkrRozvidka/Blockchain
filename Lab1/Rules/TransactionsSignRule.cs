using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class TransactionsSignRule : IRule, IValidationTransactionRule
    {
        public bool IsValid(Blockchain blockchain, Block block)
        {
            if (block == null || blockchain == null) throw new ArgumentNullException();
            bool isFirstTransaction = true;
            foreach (var transaction in block.Transactions)
            {
                if (isFirstTransaction)
                {
                    isFirstTransaction = false;
                    continue;
                }
                if (!IsValid(blockchain, transaction)) return false;
            }
            return true;
        }

        public bool IsValid(Blockchain blockchain, Transaction transaction)
        {
            if(transaction == null) throw new ArgumentNullException();
            var encryptor = new Encrypt();
            var data = JsonSerializer.Serialize(transaction.Data);
            if (!encryptor.VerifySign(data, transaction.Data.From, transaction.Sign))
                return false;
            return true;
        }
    }
}
