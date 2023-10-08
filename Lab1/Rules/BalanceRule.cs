using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class BalanceRule : IRule, IValidationTransactionRule
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
            if (transaction == null || blockchain == null) throw new ArgumentNullException();
            if (blockchain.Balances[transaction.Data.From] < transaction.Data.Amount)
                    return false;
            return true;
        }
    }
}
