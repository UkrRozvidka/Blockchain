using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Rules
{
    public class BalanceRule : IValidationTransactionRule
    {
        public bool IsValid(Node node, Block block)
        {
            if (block == null || node == null) throw new ArgumentNullException();
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
            if (transaction == null || node == null) throw new ArgumentNullException();
            if (node.Balances[transaction.Data.From] < transaction.Data.Amount)
                    return false;
            return true;
        }
    }
}
