using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class TransactionsSignRule : IValidationTransactionRule
    {
        public bool IsValid(Node node, Block block)
        {
            if (block == null || node.Blockchain == null) throw new ArgumentNullException();
            bool isFirstTransaction = true;
            foreach (var transaction in block.Transactions)
            {
                if (isFirstTransaction)
                {
                    isFirstTransaction = false;
                    continue;
                }
                if (!IsValid(node, transaction)) return false;
            }
            return true;
        }

        public bool IsValid(Node node, Transaction transaction)
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
